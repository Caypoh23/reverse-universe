using System.Collections;
using System.Collections.Generic;
using Enemies.EnemySpecific.Enemy2;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_DeadState : DeadState
{
    private readonly Enemy2 _enemy;

    public E2_DeadState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolName,
        DeadStateData stateData,
        Enemy2 enemy
    ) : base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
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

        Entity.TimeIsRewinding();

        if (Core.Stats.CurrentHealthAmount > 0)
            StateMachine.ChangeState(_enemy.IdleState);
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
