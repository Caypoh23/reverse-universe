using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform AttackPosition;
    protected bool IsAnimationFinished;

    public AttackState(Entity entity, FiniteStateMachine stateMachine, 
        string animBoolName, Transform attackPosition) : base(entity, stateMachine,
        animBoolName)
    {
        AttackPosition = attackPosition;
    }

    public override void Enter()
    {
        base.Enter();
        Entity.AnimationToState.attackState = this;
        IsAnimationFinished = false;
        Entity.SetVelocity(0f);
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

    public virtual void TriggerAttack()
    {
        
    }

    public virtual void FinishAttack()
    {
        IsAnimationFinished = true;
    }
}