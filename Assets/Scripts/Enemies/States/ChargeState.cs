using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState StateData;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsDetectingLedge;
    protected bool IsDetectingWall;
    protected bool IsChargeTimeOver;
    protected bool PerformCloseRangeAction;

    public ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) :
        base(entity, stateMachine,
            animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        IsChargeTimeOver = false;
        Entity.SetVelocity(StateData.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.chargeTime)
        {
            IsChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerInMinAgroRange = Entity.CheckPlayerInMinAgroRange();
        IsDetectingLedge = Entity.CheckLedge();
        IsDetectingWall = Entity.CheckWall();
        
        PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
    }
}