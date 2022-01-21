using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;
using UnityEngine;

namespace Player.PlayerStates.SubStates
{
    public class PlayerDashState : PlayerAbilityState
    {
        private bool _canDash;
        private bool _dashInputStop;
        private Vector2 _dashDirection;
        private float _lastDashTime;
        private Vector2 _lastAfterImagePosition;

        private readonly int _xVelocity = Animator.StringToHash("xVelocity");

        public PlayerDashState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            int animBoolId
        ) : base(playerBase, stateMachine, playerData, animBoolId) { }

        public override void Enter()
        {
            base.Enter();

            _canDash = false;

            _dashDirection = Vector2.right * Core.Movement.FacingDirection;

            PlayerBase.InputHandler.UseDashInput();
        }

        public override void Exit()
        {
            base.Exit();

            if (Core.Movement.CurrentVelocity.y > 0)
            {
                Core.Movement.SetVelocityY(
                    Core.Movement.CurrentVelocity.y * PlayerData.dashEndYMultiplier
                );
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsExitingState)
            {
                PlayerBase.Anim.SetFloat(_xVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));

                _dashInputStop = PlayerBase.InputHandler.CanDashInputStop;

                if (!_dashInputStop && IsAbilityDone)
                {
                    StartTime = Time.time;
                    Core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    PlayerBase.Rb.drag = PlayerData.drag;
                    Core.Movement.SetVelocity(PlayerData.dashVelocity, _dashDirection);
                    PlaceAfterImage();
                }

                Core.Movement.SetVelocity(PlayerData.dashVelocity, _dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= StartTime + PlayerData.dashTime)
                {
                    IsAbilityDone = true;
                    PlayerBase.Rb.drag = 0f;
                    _lastDashTime = Time.time;
                }
            }
        }

        private void CheckIfShouldPlaceAfterImage()
        {
            if (
                Vector2.Distance(PlayerBase.transform.position, _lastAfterImagePosition)
                >= PlayerData.distanceBetweenAfterImages
            )
            {
                PlaceAfterImage();
            }
        }

        private void PlaceAfterImage()
        {
            PlayerBase.ObjectPooler.SpawnFromPool(
                PlayerBase.afterImageTag,
                PlayerBase.transform.position,
                PlayerBase.transform.rotation
            );
            //_lastAfterImagePosition = Player.transform.position;
        }

        public bool CheckIfCanDash() => _canDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;

        public void ResetCanDash() => _canDash = true;
    }
}
