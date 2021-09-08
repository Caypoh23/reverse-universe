using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_IdleState : IdleState
{
    private readonly Enemy2 _enemy;
    
    public E2_IdleState(
        Entity entity, 
        FiniteStateMachine stateMachine,
        string animBoolName, 
        D_IdleState stateData,
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
        else if (IsIdleTimeOver)
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