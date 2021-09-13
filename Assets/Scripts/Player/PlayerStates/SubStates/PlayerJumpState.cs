using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;

namespace Player.PlayerStates.SubStates
{
    public class PlayerJumpState : PlayerAbilityState
    {
        private int _amountOfJumpsLeft;

        public PlayerJumpState(
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
            _amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
            PlayerBase.InputHandler.UseJumpInput();
            Core.Movement.SetVelocityY(PlayerData.jumpVelocity);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            PlayerBase.InAirState.SetIsJumping();
        }

        public bool CanJump()
        {
            if (_amountOfJumpsLeft > 0)
            {
                return true;
            }

            return false;
        }

        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;

        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}