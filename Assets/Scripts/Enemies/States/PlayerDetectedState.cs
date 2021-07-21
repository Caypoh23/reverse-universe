using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetected StateData;

    protected bool IsPlayerInMinAgroRange;
    protected bool IsPlayerInMaxAgroRange;
    protected bool PerformLongRangeAction;
    protected bool PerformCloseRangeAction;

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
        D_PlayerDetected stateData) : base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        PerformLongRangeAction = false;
        Entity.SetVelocity(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + StateData.longRangeActionTime)
        {
            PerformLongRangeAction = true;
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
        IsPlayerInMaxAgroRange = Entity.CheckPlayerInMaxAgroRange();
        
        PerformCloseRangeAction = Entity.CheckPlayerInCloseRangeAction();
    }
}