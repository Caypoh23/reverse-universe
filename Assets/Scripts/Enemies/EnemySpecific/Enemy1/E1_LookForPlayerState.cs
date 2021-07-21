using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 _enemy1;
    
    public E1_LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,
        D_LookForPlayer stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (IsPlayerIsInMinAgroRange)
        {
            StateMachine.ChangeState(_enemy1.PlayerDetectedState);
        }
        else if (IsAllTurnsTimeDone)
        {
            StateMachine.ChangeState(_enemy1.MoveState);
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