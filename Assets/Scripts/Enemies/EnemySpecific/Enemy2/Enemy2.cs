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

    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_PlayerDetected playerDetectedStateData;
    [SerializeField] private D_MeleeAttack meleeAttackStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;
    
    [SerializeField] private Transform meleeAttackPosision;

    public override void Awake()
    {
        base.Awake();
        
        MoveState = new E2_MoveState(this, StateMachine, "move", moveStateData, this);
        IdleState = new E2_IdleState(this, StateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new E2_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
        MeleeAttackState = new E2_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosision, meleeAttackStateData, this);
        LookForPlayerState = new E2_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
        
        StateMachine.Initialize(MoveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.DrawWireSphere(meleeAttackPosision.position, meleeAttackStateData.attackRadius);
    }
}