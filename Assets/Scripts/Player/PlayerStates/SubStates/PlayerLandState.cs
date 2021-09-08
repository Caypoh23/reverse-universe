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
            string animBoolName) :
            base(
                playerBase,
                stateMachine,
                playerData,
                animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsExitingState)
            {
                if (XInput != 0)
                {
                    StateMachine.ChangeState(PlayerBase.MoveState);
                }
                else if (IsAnimationFinished)
                {
                    StateMachine.ChangeState(PlayerBase.IdleState);
                }
            }
        }
    }
}