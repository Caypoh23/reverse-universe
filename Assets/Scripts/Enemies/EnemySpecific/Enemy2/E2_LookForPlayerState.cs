using System.Collections;
using System.Collections.Generic;
using Enemies.EnemySpecific.Enemy2;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_LookForPlayerState : LookForPlayerState
{
    private readonly Enemy2 _enemy;

    public E2_LookForPlayerState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolName,
        LookForPlayerData stateData,
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

        if (IsPlayerIsInMinAgroRange)
        {
            StateMachine.ChangeState(_enemy.PlayerDetectedState);
        }
        else if (IsAllTurnsTimeDone)
        {
            StateMachine.ChangeState(_enemy.MoveState);
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
