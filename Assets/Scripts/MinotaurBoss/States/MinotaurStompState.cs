using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using ObjectPool;
using UnityEngine;

public class MinotaurStompState : AttackState
{
    protected readonly MinotaurStompStateData StateData;

    protected readonly Minotaur Minotaur;

    protected GameObject EarthBump;

    protected AttackStateDamageDealer EarthBumpScript;

    public MinotaurStompState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        Transform attackPosition,
        MinotaurStompStateData stateData,
        Minotaur minotaur
    ) : base(entity, stateMachine, animBoolId, attackPosition)
    {
        Minotaur = minotaur;
        StateData = stateData;
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

    public override void FinishAttack()
    {
        base.FinishAttack();
        if (EarthBump == null)
            return;
        EarthBump.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        if (IsAnimationFinished)
        {
            if (IsPlayerMinAgroRange || IsInTouchingRange)
            {
                StateMachine.ChangeState(Minotaur.PlayerDetectedState);
            }
            else
            {
                StateMachine.ChangeState(Minotaur.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();

        EarthBump = ObjectPooler.Instance.SpawnFromPool(
            StateData.earthBumpTag,
            Minotaur.EarthBumpSpawnPosition.position,
            Minotaur.EarthBumpSpawnPosition.rotation
        );
    }
}
