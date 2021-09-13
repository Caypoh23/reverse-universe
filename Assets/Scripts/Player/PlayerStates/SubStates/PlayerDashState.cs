using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;
using UnityEngine;

namespace Player.PlayerStates.SubStates
{
    public class PlayerDashState : PlayerAbilityState
    {
        public bool CanDash { get; private set; }

        private bool _isHolding;
        private bool _dashInputStop;

        private float _lastDashTime;

        private Vector2 _dashDirection;
        private Vector2 _dashDirectionInput;
        private Vector2 _lastAfterImagePosition;

        private readonly int _xVelocity = Animator.StringToHash("xVelocity");
        private readonly int _yVelocity = Animator.StringToHash("yVelocity");

        public PlayerDashState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            string animBoolName) :
            base(
                playerBase,
                stateMachine,
                playerData,
                animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            CanDash = false;
            PlayerBase.InputHandler.UseDashInput();

            _isHolding = true;
            _dashDirection = Vector2.right * Core.Movement.FacingDirection;

            Time.timeScale = PlayerData.holdTimeScale;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            StartTime = Time.unscaledTime;
            PlayerBase.JumpState.DecreaseAmountOfJumpsLeft();
            PlayerBase.DashDirectionIndicator.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();

            if (Core.Movement.CurrentVelocity.y > 0)
            {
                Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.dashEndYMultiplier);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsExitingState)
            {
                PlayerBase.Anim.SetFloat(_yVelocity, Core.Movement.CurrentVelocity.y);
                PlayerBase.Anim.SetFloat(_xVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));

                if (_isHolding)
                {
                    _dashDirectionInput = PlayerBase.InputHandler.DashDirectionInput;
                    _dashInputStop = PlayerBase.InputHandler.CanDashInputStop;

                    if (_dashDirectionInput != Vector2.zero)
                    {
                        _dashDirection = _dashDirectionInput;
                        _dashDirection.Normalize();
                    }

                    var angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                    PlayerBase.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                    if (_dashInputStop || Time.unscaledTime >= StartTime + PlayerData.maxHoldTime)
                    {
                        _isHolding = false;
                        Time.timeScale = 1f;
                        StartTime = Time.time;
                        Core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                        PlayerBase.Rb.drag = PlayerData.drag;
                        Core.Movement.SetVelocity(PlayerData.dashVelocity, _dashDirection);
                        PlayerBase.DashDirectionIndicator.gameObject.SetActive(false);
                        PlaceAfterImage();
                    }
                }
                else
                {
                    Core.Movement.SetVelocity(PlayerData.dashVelocity, _dashDirection);
                    CheckIfShouldPlaceAfterImage();

                    if (Time.time >= StartTime + PlayerData.dashTime)
                    {
                        PlayerBase.Rb.drag = 0f;
                        IsAbilityDone = true;
                        _lastDashTime = Time.time;
                    }
                }
            }
        }

        private void CheckIfShouldPlaceAfterImage()
        {
            if (Vector2.Distance(PlayerBase.transform.position, _lastAfterImagePosition) >=
                PlayerData.distanceBetweenAfterImages)
            {
                PlaceAfterImage();
            }
        }

        private void PlaceAfterImage()
        {
            PlayerBase.ObjectPooler.SpawnFromPool(PlayerBase.afterImageTag, PlayerBase.transform.position,
                PlayerBase.transform.rotation);
            //_lastAfterImagePosition = Player.transform.position;
        }

        public bool CheckIfCanDash()
        {
            return CanDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;
        }

        public void ResetCanDash() => CanDash = true;
    }
}