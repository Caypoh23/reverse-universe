using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 _enemy1;

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
        D_PlayerDetected stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy1 = enemy;
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
            StateMachine.ChangeState(_enemy1.MeleeAttackState);
        }
        else if (PerformLongRangeAction)
        {
            StateMachine.ChangeState(_enemy1.ChargeState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(_enemy1.LookForPlayerState);
        }
        else if (!IsDetectingLedge)
        {
            Entity.Flip();
            StateMachine.ChangeState(_enemy1.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}