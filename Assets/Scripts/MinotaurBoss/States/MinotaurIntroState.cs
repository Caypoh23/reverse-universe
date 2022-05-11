using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using UnityEngine;

public class MinotaurIntroState : State
{
    protected readonly BossIntroStateData StateData;
    private readonly Minotaur _minotaur;

    protected bool IsIntroTimeOver;
    protected float IntroTime;

    public MinotaurIntroState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolName,
        BossIntroStateData stateData,
        Minotaur minotaur
    ) : base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
        _minotaur = minotaur;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        if (!Core.Movement.IsRewinding)
        {
            Core.Movement.SetVelocityX(0f);
        }

        IsIntroTimeOver = false;

        IntroTime = StateData.introDuration;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        Core.Movement.SetVelocityY(Mathf.Lerp(-1, -20, 3));

        if (Core.CollisionSenses.IsGrounded)
        {
            _minotaur.LandFeedback.PlayFeedbacks();

            if (Time.time >= StartTime + IntroTime)
            {
                IsIntroTimeOver = true;
            }
        }

        if (IsIntroTimeOver)
        {
            StateMachine.ChangeState(_minotaur.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
