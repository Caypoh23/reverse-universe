using System;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurPlayerDetectedState : PlayerDetectedState
{
    private readonly Minotaur _minotaur;

    public MinotaurPlayerDetectedState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        PlayerDetectedData stateData,
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

        CheckFirstAttackPhase();

        CheckSecondAttackPhase();

        if (IsInTouchingRange || PerformLongRangeAction)
        {
            StateMachine.ChangeState(_minotaur.ChargeState);
        }
        else if (!IsPlayerInMaxAgroRange)
        {
            StateMachine.ChangeState(_minotaur.LookForPlayerState);
        }
        else if (!IsDetectingLedge)
        {
            Core.Movement.Flip();
            StateMachine.ChangeState(_minotaur.MoveState);
        }

        Debug.Log(_minotaur.GenerateRandomNumber());
    }

    private void CheckFirstAttackPhase()
    {
        if (_minotaur.GenerateRandomNumber() <= 25 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.SwingAttackState);
        }
        else if (_minotaur.GenerateRandomNumber() <= 50 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.PoundAttackState);
        }
    }

    private void CheckSecondAttackPhase()
    {
        if (!_minotaur.IsInSecondPhase())
            return;

        if (
            (_minotaur.GenerateRandomNumber() > 50 && _minotaur.GenerateRandomNumber() <= 75)
            && PerformCloseRangeAction
        )
        {
            StateMachine.ChangeState(_minotaur.StompState);
        }
        else if (
            _minotaur.GenerateRandomNumber() > 75
            && (PerformLongRangeAction || PerformCloseRangeAction)
        )
        {
            StateMachine.ChangeState(_minotaur.ChargeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
