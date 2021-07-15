using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class 
public class PlayerState
{
    // Name should be changed
    protected Player PlayerScript;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;

    // How long we have been in specific state
    protected float StartTime;

    private string _animBoolName;

    public PlayerState(Player playerScript, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName)
    {
        this.PlayerScript = playerScript;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        PlayerScript.Anim.SetBool(_animBoolName, true);
        StartTime = Time.time;
    }

    public virtual void Exit()
    {
        PlayerScript.Anim.SetBool(_animBoolName, false);
    }

    // Update
    public virtual void LogicUpdate()
    {
    }

    // Fixed Update
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    // Check for wall, ground, ledge etc.
    public virtual void DoChecks()
    {
    }
}