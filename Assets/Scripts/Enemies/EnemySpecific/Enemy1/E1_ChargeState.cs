using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class E1_ChargeState : ChargeState
{
    private Enemy1 _enemy1;

    public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData,
        Enemy1 enemy)
        : base(entity, stateMachine, animBoolName, stateData)
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
        else if (!IsDetectingLedge || IsDetectingWall)
        {
            StateMachine.ChangeState(_enemy1.LookForPlayerState);
        }
        else if (IsChargeTimeOver)
        {
            if (IsPlayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy1.PlayerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(_enemy1.LookForPlayerState);
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