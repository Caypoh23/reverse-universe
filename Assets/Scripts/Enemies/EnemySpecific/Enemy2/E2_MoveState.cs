using System.Collections;
using System.Collections.Generic;
using Enemies.EnemySpecific.Enemy2;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private readonly Enemy2 _enemy;

    public E2_MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        MoveStateData stateData,
        Enemy2 enemy) :
        base(
            entity,
            stateMachine,
            animBoolId,
            stateData)
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

        if(Core.Movement.IsRewinding) return;

        
        if(Core.Stats.CurrentHealthAmount <= 0)
        {
            StateMachine.ChangeState(_enemy.DeadState);
        }
        else if (IsPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(_enemy.PlayerDetectedState);
        }
        else if (IsDetectingWall || !IsDetectingLedge)
        {
            _enemy.IdleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(_enemy.IdleState);
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