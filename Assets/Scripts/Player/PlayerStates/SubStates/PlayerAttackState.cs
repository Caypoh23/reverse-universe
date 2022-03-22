using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;
using Player.Weapons;

namespace Player.PlayerStates.SubStates
{
    public class PlayerAttackState : PlayerAbilityState
    {
        private Weapon _weapon;

        private int _xInput;

        private float _velocityToSet;
        private bool _setVelocity;
        private bool _shouldCheckFlip;

        public PlayerAttackState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            int animBoolId
        ) : base(playerBase, stateMachine, playerData, animBoolId) { }

        public override void Enter()
        {
            base.Enter();

            _setVelocity = false;

            _weapon.EnterWeapon();
        }

        public override void Exit()
        {
            base.Exit();

            _weapon.ExitWeapon();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Core.Movement.IsRewinding)
            {
                return;
            }

            _xInput = PlayerBase.InputHandler.NormalizedInputX;

            if (_shouldCheckFlip)
            {
                Core.Movement.CheckIfShouldFlip(_xInput);
            }

            if (_setVelocity)
            {
                Core.Movement.SetVelocityX(_velocityToSet * Core.Movement.FacingDirection);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _weapon = weapon;

            weapon.InitializeWeapon(this, Core);
        }

        public void SetPlayerVelocity(float velocity)
        {
            Core.Movement.SetVelocityX(velocity * Core.Movement.FacingDirection);

            _velocityToSet = velocity;
            _setVelocity = true;
        }

        public void SetFlipCheck(bool value)
        {
            _shouldCheckFlip = value;
        }

        #region Animation Triggers

        public override void AnimationFinishedTrigger()
        {
            base.AnimationFinishedTrigger();

            IsAbilityDone = true;
        }
        
        #endregion
    }
}
