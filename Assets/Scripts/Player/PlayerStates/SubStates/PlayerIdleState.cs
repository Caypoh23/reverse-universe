using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;

namespace Player.PlayerStates.SubStates
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(
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

        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityX(0.0f);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Core.Movement.IsRewinding)
            {
                return;
            }

            if (XInput != 0 && !IsExitingState)
            {
                StateMachine.ChangeState(PlayerBase.MoveState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void DoChecks()
        {
            base.DoChecks();
        }
    }
}