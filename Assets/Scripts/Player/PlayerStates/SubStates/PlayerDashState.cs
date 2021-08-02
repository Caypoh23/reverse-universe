using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }

    private bool _isHolding;
    private bool _dashInputStop;

    private float _lastDashTime;

    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector2 _lastAfterImagePosition;

    private readonly int _xVelocity = Animator.StringToHash("xVelocity");
    private readonly int _yVelocity = Animator.StringToHash("yVelocity");

    public PlayerDashState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        Player.InputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * Player.FacingDirection;

        Time.timeScale = PlayerData.holdTimeScale;
        StartTime = Time.unscaledTime;

        Player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if (Player.CurrentVelocity.y > 0)
        {
            Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!IsExitingState)
        {
            Player.Anim.SetFloat(_yVelocity, Player.CurrentVelocity.y);
            Player.Anim.SetFloat(_xVelocity, Mathf.Abs(Player.CurrentVelocity.x));

            if (_isHolding)
            {
                _dashDirectionInput = Player.InputHandler.DashDirectionInput;
                _dashInputStop = Player.InputHandler.DashInputStop;

                if (_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }

                var angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
                Player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if (_dashInputStop || Time.unscaledTime >= StartTime + PlayerData.maxHoldTime)
                {
                    _isHolding = false;
                    Time.timeScale = 1f;
                    StartTime = Time.time;
                    Player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                    Player.Rb.drag = PlayerData.drag;
                    Player.SetVelocity(PlayerData.dashVelocity, _dashDirection);
                    Player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                Player.SetVelocity(PlayerData.dashVelocity, _dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= StartTime + PlayerData.dashTime)
                {
                    Player.Rb.drag = 0f;
                    IsAbilityDone = true;
                    _lastDashTime = Time.time;
                    
                }
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(Player.transform.position, _lastAfterImagePosition) >=
            PlayerData.distanceBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        Player.ObjectPooler.SpawnFromPool(Player.afterImageTag, Player.transform.position, Player.transform.rotation);
        //_lastAfterImagePosition = Player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
}