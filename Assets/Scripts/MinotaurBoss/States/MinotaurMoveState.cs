using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurMoveState : MoveState
{
    private readonly Minotaur _minotaur;

    public MinotaurMoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        MoveStateData stateData,
        Minotaur minotaur
    ) : base(entity, stateMachine, animBoolId, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (IsPlayerInMinAgroRange || IsInTouchingRange)
        {
            StateMachine.ChangeState(_minotaur.PlayerDetectedState);
        }
        else if (IsDetectingWall || !IsDetectingLedge)
        {
            _minotaur.IdleState.SetFlipAfterIdle(true);
            StateMachine.ChangeState(_minotaur.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
