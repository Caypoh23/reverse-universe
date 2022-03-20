using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class MinotaurDeadState : DeadState
{
    private readonly Minotaur _minotaur;

    public MinotaurDeadState(
        Entity entity,
        FiniteStateMachine stateMachine,
        int animBoolId,
        DeadStateData stateData,
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
        //_minotaur.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
