using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;

namespace Player.PlayerStates.SubStates
{
    public class PlayerLandState : PlayerGroundedState
    {
        public PlayerLandState(
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
                if (XInput != 0 && !Core.Movement.IsRewinding)
                {
                    StateMachine.ChangeState(PlayerBase.MoveState);
                }
                else if (IsAnimationFinished && !Core.Movement.IsRewinding)
                {
                    StateMachine.ChangeState(PlayerBase.IdleState);
                }
            }
        }
    }
}