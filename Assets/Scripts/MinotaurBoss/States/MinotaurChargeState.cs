using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurChargeState : ChargeState
{
    private readonly Minotaur _minotaur;

    public MinotaurChargeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        ChargeStateData stateData,
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
        _minotaur.ActivateChargeCollider();
    }

    public override void Exit()
    {
        base.Exit();
        _minotaur.DeactivateChargeCollider();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckIfCanStun();

        CheckLedge();

        OnChargeTimeOver();

        CheckFirstAttackPhase();

        CheckSecondAttackPhase();
    }

    private void CheckIfCanStun()
    {
        if (Core.CollisionSenses.IsCheckingWall)
        {
            _minotaur.LandFeedback?.PlayFeedbacks();
            StateMachine.ChangeState(_minotaur.StunState);
        }
    }

    private void CheckLedge()
    {
        if (!Core.CollisionSenses.IsCheckingLedge)
        {
            StateMachine.ChangeState(_minotaur.IdleState);
        }
    }

    private void OnChargeTimeOver()
    {
        if (IsChargeTimeOver && (IsInTouchingRange || IsPlayerInMinAgroRange))
        {
            StateMachine.ChangeState(_minotaur.PlayerDetectedState);
        }
        else if (IsChargeTimeOver)
        {
            StateMachine.ChangeState(_minotaur.IdleState);
        }
    }

    private void CheckFirstAttackPhase()
    {
        if (_minotaur.GenerateRandomNumber() <= 25 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.PoundAttackState);
        }
        else if (_minotaur.GenerateRandomNumber() <= 50 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.SwingAttackState);
        }
    }

    private void CheckSecondAttackPhase()
    {
        if (!_minotaur.IsInSecondPhase())
            return;

        if (_minotaur.GenerateRandomNumber() <= 75 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.StompState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
