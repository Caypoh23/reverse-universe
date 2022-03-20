using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurSwingAttackState : MeleeAttackState
{
    private readonly Minotaur _minotaur;

    public MinotaurSwingAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        Transform attackPosition,
        MeleeAttackData stateData,
        Minotaur minotaur
    ) : base(entity, stateMachine, animBoolId, attackPosition, stateData)
    {
        _minotaur = minotaur;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (IsAnimationFinished)
        {
            if (IsPlayerMinAgroRange || IsInTouchingRange)
            {
                StateMachine.ChangeState(_minotaur.PlayerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(_minotaur.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
