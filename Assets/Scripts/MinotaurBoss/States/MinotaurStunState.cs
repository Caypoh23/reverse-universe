using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

public class MinotaurStunState : StunState
{
    private readonly Minotaur _minotaur;

    public MinotaurStunState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        StunStateData stateData,
        Minotaur minotaur
    ) : base(entity, stateMachine, animBoolId, stateData)
    {
        _minotaur = minotaur;
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

        if (Core.Movement.IsRewinding)
            return;

        if (Core.Stats.CurrentHealthAmount <= 0)
            StateMachine.ChangeState(_minotaur.DeadState);

        if (IsStunTimeOver)
        {
            if (PerformCloseRangeAction)
            {
                StateMachine.ChangeState(_minotaur.PoundAttackState);
            }
            else if (IsPLayerInMinAgroRange)
            {
                StateMachine.ChangeState(_minotaur.ChargeState);
            }
            else
            {
                _minotaur.LookForPlayerState.SetTurnImmediately(true);
                StateMachine.ChangeState(_minotaur.LookForPlayerState);
            }
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
