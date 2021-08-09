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
        Core.Movement.SetVelocityX(StateData.movementSpeed * Core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Core.Movement.SetVelocityX(StateData.movementSpeed * Core.Movement.FacingDirection);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        IsDetectingLedge = Core.CollisionSenses.Ledge;
        IsDetectingWall = Core.CollisionSenses.WallFront;
        
        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }
}