using System.Collections;
using System.Collections.Generic;
using Enemies.EnemySpecific.Enemy2;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_MeleeAttackState : MeleeAttackState
{
    private readonly Enemy2 _enemy;

    public E2_MeleeAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolName,
        Transform attackPosition,
        MeleeAttackData stateData,
        Enemy2 enemy
    ) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
    {
        _enemy = enemy;
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

        if (Core.Stats.CurrentHealthAmount <= 0)
        {
            StateMachine.ChangeState(_enemy.DeadState);
        }

        if (IsAnimationFinished && !Core.Movement.IsRewinding)
        {
            if (IsPlayerMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else if (!IsPlayerMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.LookForPlayerState);
            }
        }
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
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }
}
