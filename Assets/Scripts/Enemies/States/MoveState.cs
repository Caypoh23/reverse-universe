using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState StateData;

    protected bool IsDetectingWall;
    protected bool IsDetectingLedge;
    protected bool IsPlayerInMinAgroRange;

    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(
        entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.SetVelocity(StateData.movementSpeed);
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
        
        
        IsDetectingLedge = Entity.CheckLedge();
        IsDetectingWall = Entity.CheckWall();
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }
}