using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 _enemy;

    public E1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData,
        Enemy1 enemy) :
        base(entity, stateMachine, animBoolName, stateData)
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
}