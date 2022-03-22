using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurPoundAttackState : MeleeAttackState
{
    private readonly Minotaur _minotaur;

    public MinotaurPoundAttackState(
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
        //_minotaur.ActivateExplosion();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Entity.TimeIsRewinding();
        
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
