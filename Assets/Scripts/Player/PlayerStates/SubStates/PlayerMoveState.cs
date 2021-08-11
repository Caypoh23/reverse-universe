using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;

namespace Player.PlayerStates.SubStates
{
    public class PlayerMoveState : PlayerGroundedState
    {
        public PlayerMoveState(PlayerBase playerBase, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerBase, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(PlayerData.movementVelocity * XInput);

            if (XInput == 0 && !IsExitingState)
            {
                StateMachine.ChangeState(PlayerBase.IdleState);
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