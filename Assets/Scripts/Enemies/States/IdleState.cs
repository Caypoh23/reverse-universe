using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class IdleState : State
{
    protected D_IdleState StateData;

    protected bool FlipAfterIdle;
    protected bool IsIdleTimeOver;
    protected bool IsPlayerInMinAgroRange;

    protected float IdleTime;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(
        entity, stateMachine,
        animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        Entity.SetVelocity(0f);
        IsIdleTimeOver = false;

        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (FlipAfterIdle)
        {
            Entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= StartTime + IdleTime)
        {
            IsIdleTimeOver = true;
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
    }

    public void SetFlipAfterIdle(bool flip)
    {
        FlipAfterIdle = flip;
    }

    public void SetRandomIdleTime()
    {
        IdleTime = UnityEngine.Random.Range(StateData.minIdleTime, StateData.maxIdleTime);
    }
}