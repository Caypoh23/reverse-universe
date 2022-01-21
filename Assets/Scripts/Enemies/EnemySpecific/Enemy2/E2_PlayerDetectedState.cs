using System.Collections;
using System.Collections.Generic;
using Enemies.EnemySpecific.Enemy2;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_PlayerDetectedState : PlayerDetectedState
{
    private Enemy2 _enemy;

    public E2_PlayerDetectedState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        PlayerDetectedData stateData,
        Enemy2 enemy) :
        base(
            entity,
            stateMachine,
            animBoolName,
            stateData)
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

        if (PerformCloseRangeAction)
        {
            if (Time.time >= _enemy.DodgeState.StartTime + _enemy.dodgeStateData.dodgeCooldown && !Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.DodgeState);
            }
            else if(!Core.Movement.IsRewinding)
            {
                StateMachine.ChangeState(_enemy.MeleeAttackState);
            }
        }
        else if (PerformLongRangeAction && !Core.Movement.IsRewinding)
        {
            StateMachine.ChangeState(_enemy.RangedAttackState);
        }
        else if (!IsPlayerInMaxAgroRange && !Core.Movement.IsRewinding)
        {
            StateMachine.ChangeState(_enemy.LookForPlayerState);
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
}