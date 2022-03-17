using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurStompState : State
{
    protected readonly RangedAttackStateData StateData;

    public MinotaurStompState(Entity entity, FiniteStateMachine stateMachine, int animBoolName)
        : base(entity, stateMachine, animBoolName) { }

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

        if(Core.Movement.IsRewinding) return;


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
