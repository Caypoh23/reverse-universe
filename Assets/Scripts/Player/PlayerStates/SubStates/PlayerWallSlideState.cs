using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;

namespace Player.PlayerStates.SubStates
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        public bool IsWallSliding;

        public PlayerWallSlideState(
            PlayerBase playerBase,
            PlayerStateMachine stateMachine,
            PlayerData playerData,
            int animBoolId) :
            base(
                playerBase,
                stateMachine,
                playerData,
                animBoolId)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsExitingState)
            {
                IsWallSliding = true;
                Core.Movement.SetVelocityY(-PlayerData.wallSlideVelocity);
            }
        }

        public override void Exit()
        {
            base.Exit();

            IsWallSliding = false;
        }
    }
}