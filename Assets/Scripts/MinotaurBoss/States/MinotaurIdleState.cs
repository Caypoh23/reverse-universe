using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurIdleState : IdleState
{
    private readonly Minotaur _minotaur;

    public MinotaurIdleState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        IdleStateData stateData,
        Minotaur minotaur
    ) : base(entity, stateMachine, animBoolId, stateData)
    {
        _minotaur = minotaur;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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
        {
            StateMachine.ChangeState(_minotaur.DeadState);
        }
        if (IsPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(_minotaur.PlayerDetectedState);
        }
        else if (IsIdleTimeOver)
        {
            StateMachine.ChangeState(_minotaur.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
