using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{
    private Enemy1 _enemy;
    
    public E1_StunState(Entity entity, FiniteStateMachine stateMachine, 
        string animBoolName, D_StunState stateData, Enemy1 enemy) :
        base(entity, stateMachine, animBoolName, stateData)
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
            if (PerformCloseRangeAction)
            {
                StateMachine.ChangeState(_enemy.MeleeAttackState);                
            }
            else if (IsPLayerInMinAgroRange)
            {
                StateMachine.ChangeState(_enemy.ChargeState);
            }
            else
            {
                _enemy.LookForPlayerState.SetTurnImmediately(true);
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