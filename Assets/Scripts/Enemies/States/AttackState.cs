using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform AttackPosition;
    
    protected bool IsAnimationFinished;
    protected bool IsPlayerMinAgroRange;

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
        Core.Movement.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Core.Movement.SetVelocityX(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        IsPlayerMinAgroRange = Entity.CheckPlayerInMinAgroRange();
    }

    public virtual void TriggerAttack()
    {
        
    }

    public virtual void FinishAttack()
    {
        IsAnimationFinished = true;
    }
}