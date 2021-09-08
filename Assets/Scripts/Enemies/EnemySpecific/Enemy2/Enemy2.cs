using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States;
using Enemies.States.Data;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState MoveState { get; private set; }
    public E2_IdleState IdleState { get; private set; }
    public E2_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E2_MeleeAttackState MeleeAttackState { get; private set; }
    public E2_LookForPlayerState LookForPlayerState { get; private set; }
    public E2_StunState StunState { get; private set; }
    public E2_DeadState DeadState { get; private set; }

    [SerializeField] private MoveStateData moveStateData;
    [SerializeField] private IdleStateData idleStateData;
    [SerializeField] private PlayerDetectedData playerDetectedStateData;
    [SerializeField] private MeleeAttackData meleeAttackStateData;
    [SerializeField] private LookForPlayerData lookForPlayerStateData;
    [SerializeField] private StunStateData stunStateData;
    [SerializeField] private DeadStateData deadStateData;

    [SerializeField] private Transform meleeAttackPosision;

    public override void Awake()
    {
        base.Awake();

        MoveState = new E2_MoveState(this, StateMachine, "move", moveStateData, this);
        IdleState = new E2_IdleState(this, StateMachine, "idle", idleStateData, this);
        PlayerDetectedState =
            new E2_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
        MeleeAttackState = new E2_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosision,
            meleeAttackStateData, this);
        LookForPlayerState =
            new E2_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
        StunState =
            new E2_StunState(this, StateMachine, "stun", stunStateData, this);
        DeadState =
            new E2_DeadState(this, StateMachine, "dead", deadStateData, this);
    }

    private void Start()
    {
        StateMachine.Initialize(MoveState);
    }


    public override void DamageHop(float velocity)
    {
        base.DamageHop(velocity);

        if (IsDead)
        {
            StateMachine.ChangeState(DeadState);
        }
        else if (IsStunned && StateMachine.CurrentState != StunState)
        {
            StateMachine.ChangeState(StunState);
        }
        else if (!CheckPlayerInMinAgroRange())
        {
            LookForPlayerState.SetTurnImmediately(true);
            StateMachine.ChangeState(LookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosision.position, meleeAttackStateData.attackRadius);
    }
}