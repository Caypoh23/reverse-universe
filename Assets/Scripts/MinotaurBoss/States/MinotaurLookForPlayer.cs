using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;

public class MinotaurLookForPlayer : LookForPlayerState
{
    private readonly Minotaur _minotaur;

    public MinotaurLookForPlayer(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        LookForPlayerData stateData,
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

        if (IsPlayerIsInMinAgroRange || IsInTouchingRange)
        {
            StateMachine.ChangeState(_minotaur.PlayerDetectedState);
        }
        else if (IsAllTurnsTimeDone)
        {
            StateMachine.ChangeState(_minotaur.MoveState);
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
