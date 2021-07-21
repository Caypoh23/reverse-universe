﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForPlayerState LookForPlayerState { get; private set; }

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetected playerDetectedData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayer lookForPlayerStateData;

    public override void Start()
    {
        base.Start();

        MoveState = new E1_MoveState(this, StateMachine, "move", moveStateData, this);
        IdleState = new E1_IdleState(this, StateMachine, "idle", idleStateData, this);
        PlayerDetectedState =
            new E1_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedData, this);
        ChargeState = new E1_ChargeState(this, StateMachine, "charge", chargeStateData, this);
        LookForPlayerState =
            new E1_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
        StateMachine.Initialize(MoveState);
    }
}