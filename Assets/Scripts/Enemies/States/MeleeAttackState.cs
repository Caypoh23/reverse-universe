using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttack StateData;


    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
        Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        StateData = stateData;
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        var detectedObjects =
            Physics2D.OverlapCircleAll(AttackPosition.position, StateData.attackRadius, StateData.whatIsPlayer);

    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}