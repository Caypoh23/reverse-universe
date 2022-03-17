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

        if (Core.Movement.IsRewinding)
            return;

        if (Core.Stats.CurrentHealthAmount <= 0)
        {
            StateMachine.ChangeState(_minotaur.DeadState);
        }
        else if (_minotaur.GenerateRandomNumber() <= 50 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.SwingAttackState);
        }
        else if (_minotaur.GenerateRandomNumber() > 50 && PerformCloseRangeAction)
        {
            StateMachine.ChangeState(_minotaur.PoundAttackState);
        }
        else if (PerformLongRangeAction)
        {
            StateMachine.ChangeState(_minotaur.ChargeState);
        }
        else if (!IsDetectingLedge)
        {
            Core.Movement.Flip();
            StateMachine.ChangeState(_minotaur.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
