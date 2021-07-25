using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState StateData;
    
    public DeadState(Entity entity, FiniteStateMachine stateMachine,
        string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        StateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        GameObject.Instantiate(StateData.deathBloodParticle, Entity.AliveGO.transform.position, StateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(StateData.deathBloodParticle, Entity.AliveGO.transform.position, StateData.deathBloodParticle.transform.rotation);

        Entity.gameObject.SetActive(false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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
