using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class StunState : State
{
    protected D_StunState StateData;

    protected bool IsStunTimeOver;
    protected bool IsGrounded;
    protected bool IsMovementStopped;
    protected bool PerformCloseRangeAction;
    protected bool IsPLayerInMinAgroRange;
    
    public StunState(Entity entity, FiniteStateMachine stateMachine,
        string animBoolName, D_StunState stateData) : base(entity, stateMachine,
        animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsStunTimeOver = false;
        IsMovementStopped = false;
        Entity.SetVelocity(StateData.stunKnockbackSpeed, StateData.stunKnockbackAngle, Entity.LastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        
        Entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.stunTime)
        {
            IsStunTimeOver = true;
        }

        if (IsGrounded && Time.time >= StartTime + StateData.stunKnockbackTime && !IsMovementStopped)
        {
            IsMovementStopped = true;
            Entity.SetVelocity(0f);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        IsGrounded = Entity.CheckGround();
        PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
        IsPLayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }
}