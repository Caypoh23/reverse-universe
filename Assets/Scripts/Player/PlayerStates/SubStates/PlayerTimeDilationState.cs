using System.Collections;
using System.Collections.Generic;
using Player.Data;
using Player.PlayerFiniteStateMachine;
using Player.SuperStates;
using UnityEngine;

public class PlayerTimeDilationState : PlayerAbilityState
{
    public bool CanDelayTime { get; private set; }

    private bool _isHolding;
    private bool _timeDilationInputStop;

    private float _lastTimeDilationDuration;
    
    public PlayerTimeDilationState(
        PlayerBase playerBase,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animBoolName) :
        base(
            playerBase,
            stateMachine,
            playerData,
            animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDelayTime = false;
        PlayerBase.InputHandler.UseTimeDilationInput();
        _isHolding = true;

        Time.timeScale = PlayerData.timeDilationTimeScale;
        // smooth time dilation
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        StartTime = Time.unscaledTime;
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            if (_isHolding)
            {
                _timeDilationInputStop = PlayerBase.InputHandler.TimeDilationInputStop;

                if (_timeDilationInputStop || Time.unscaledTime >= StartTime + PlayerData.maxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                }
            }
            else
            {
                if (Time.time >= StartTime + PlayerData.timeDilationDuration)
                {
                    IsAbilityDone = true;
                    _lastTimeDilationDuration = Time.time;
                }
            }
        }
    }
    
    public bool CheckIfCanDelayTime()
    {
        return CanDelayTime && Time.time >= _lastTimeDilationDuration + PlayerData.dashCooldown;
    }
    public void ResetCanDelayTime() => CanDelayTime = true;

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}