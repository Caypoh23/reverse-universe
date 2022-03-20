using System.Collections;
using System.Collections.Generic;
using Enemies.EnemySpecific.Enemy2;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class E2_StunState : StunState
{
    private readonly Enemy2 _enemy;

    public E2_StunState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        StunStateData stateData,
        Enemy2 enemy
    ) : base(entity, stateMachine, animBoolId, stateData)
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

        if (IsStunTimeOver)
        {
            if (IsPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.PlayerDetectedState);
            }
            else
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
}
