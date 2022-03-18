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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Core.Movement.IsRewinding)
            return;

        if (Core.Stats.CurrentHealthAmount <= 0)
        {
            StateMachine.ChangeState(_minotaur.DeadState);
        }
        else if (_minotaur.GenerateRandomNumber() <= 25 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.PoundAttackState);
        }
        else if (_minotaur.GenerateRandomNumber() <= 50 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.SwingAttackState);
        }
        if (_minotaur.GenerateRandomNumber() > 50 &&PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.StompState);
        }
        else if(Core.CollisionSenses.IsCheckingWall || !Core.CollisionSenses.IsCheckingLedge)
        {
            StateMachine.ChangeState(_minotaur.IdleState);
        }
        else if (IsChargeTimeOver && IsPlayerInMinAgroRange)
        {
            StateMachine.ChangeState(_minotaur.PlayerDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
