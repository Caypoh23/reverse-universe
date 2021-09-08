using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 _enemy;

    public E2_MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        MoveStateData stateData,
        Enemy2 enemy) :
        base(
            entity,
            stateMachine,
            animBoolName,
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


        if (IsPlayerInMinAgroRange)
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