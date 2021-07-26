using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make abstract
public abstract class PlayerState
{
    // Name should be changed
    // _private
    protected Player Player; // animator
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;
    protected bool IsAnimationFinished;
    protected bool IsExitingState;

    protected float StartTime;

    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName)
    {
        Player = player;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        Player.Anim.SetBool(_animBoolName, true);
        StartTime = Time.time;
        IsAnimationFinished = false;
        IsExitingState = false;
    }

    public virtual void Exit()
    {
        Player.Anim.SetBool(_animBoolName, false);
        IsExitingState = true;
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
    // Tick
    // OnUpdate
    // Rename
    public virtual void DoChecks()
    {
    }

    public virtual void AnimationTrigger()
    {
    }

    public virtual void AnimationFinishedTrigger() => IsAnimationFinished = true;
}