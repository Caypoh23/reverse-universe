using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_DeadState : DeadState
{
    private Enemy2 _enemy;
    
    public E2_DeadState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        DeadStateData stateData,
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