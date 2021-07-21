﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine StateMachine;
    protected Entity Entity;

    protected float StartTime;

    protected string AnimBoolName;

    // Constructor

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        Entity = entity;
        StateMachine = stateMachine;
        AnimBoolName = animBoolName;
    }

    // States
    public virtual void Enter()
    {
        StartTime = Time.time;
        Entity.Anim.SetBool(AnimBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        Entity.Anim.SetBool(AnimBoolName, false);
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
    }
}