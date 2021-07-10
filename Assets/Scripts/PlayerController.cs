using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _movementInputDirection;
    private float _jumpTimer;
    private float _turnTimer;
    private float _wallJumpTimer;
    private float _dashTimeLeft;
    private float _lastImageXpos;
    private float _lastDash = -100;

    private int _amountOfJumpsLeft;
    private int _facingDirection = 1;
    private int _lastWallJumpDirection;

    private bool _isFacingRight = true;
    private bool _isWalking;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _isWallSliding;
    private bool _canNormalJump;
    private bool _canWallJump;
    private bool _isAttemptingToJump;
    private bool _checkJumpMultiplier;
    private bool _canMove;
    private bool _canFlip;
    private bool _hasWallJumped;
    private bool _isTouchingLedge;
    private bool _canClimbLedge = false;
    private bool _ledgeDetected;
    private bool _isDashing = false;

    private Vector2 _ledgePosBot;
    private Vector2 _ledgePos1;
    private Vector2 _ledgePos2;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [SerializeField] private int amountOfJumps = 1;
    
    
    [SerializeField] private Tag afterImageTag;

    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float jumpForce = 16.0f;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float movementForceInAir;
    [SerializeField] private float airDragMultiplier = 0.95f;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] private float wallHopForce;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float jumpTimerSet = 0.15f;
    [SerializeField] private float turnTimerSet = 0.1f;
    [SerializeField] private float wallJumpTimerSet = 0.5f;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashCooldown;

    [SerializeField] private float ledgeClimbXOffset1 = 0f;
    [SerializeField] private float ledgeClimbYOffset1 = 0f;
    [SerializeField] private float ledgeClimbXOffset2 = 0f;
    [SerializeField] private float ledgeClimbYOffset2 = 0f;

    [SerializeField] private Vector2 wallHopDirection;
    [SerializeField] private Vector2 wallJumpDirection;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    [SerializeField] private LayerMask whatIsGround;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsWallSliding = Animator.StringToHash("isWallSliding");
    private static readonly int CanClimbLedge = Animator.StringToHash("canClimbLedge");

    private void Start()
    {
        _amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimb();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        if (_isTouchingWall && _movementInputDirection == _facingDirection && rb.velocity.y < 0 && !_canClimbLedge)
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void CheckLedgeClimb()
    {
        if (_ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;

            if (_isFacingRight)
            {
                _ledgePos1 = new Vector2(Mathf.Floor(_ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1,
                    Mathf.Floor(_ledgePosBot.y) + ledgeClimbYOffset1);
                _ledgePos2 = new Vector2(Mathf.Floor(_ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2,
                    Mathf.Floor(_ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else
            {
                _ledgePos1 = new Vector2(Mathf.Ceil(_ledgePosBot.x - wallCheckDistance) +
                                         ledgeClimbXOffset1, Mathf.Floor(_ledgePosBot.y) +
                                                             ledgeClimbYOffset1);
                _ledgePos2 = new Vector2(Mathf.Ceil(_ledgePosBot.x - wallCheckDistance) -
                                         ledgeClimbXOffset2, Mathf.Floor(_ledgePosBot.y) +
                                                             ledgeClimbYOffset2);
            }

            _canMove = false;
            _canFlip = false;

            anim.SetBool(CanClimbLedge, _canClimbLedge);
        }

        if (_canClimbLedge)
        {
            transform.position = _ledgePos1;
        }
    }

    public void FinishLedgeClimb()
    {
        _canClimbLedge = false;
        transform.position = _ledgePos2;
        _canMove = true;
        _canFlip = true;
        _ledgeDetected = false;
        anim.SetBool(CanClimbLedge, _canClimbLedge);
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        _isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        _isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if (_isTouchingWall && !_isTouchingLedge && !_ledgeDetected)
        {
            _ledgeDetected = true;
            _ledgePosBot = wallCheck.position;
        }
    }


    private void CheckIfCanJump()
    {
        if (_isGrounded && rb.velocity.y <= 0.01f)
        {
            _amountOfJumpsLeft = amountOfJumps;
        }

        if (_isTouchingWall)
        {
            _canWallJump = true;
        }

        _canNormalJump = _amountOfJumpsLeft > 0;
    }


    private void CheckMovementDirection()
    {
        if (_isFacingRight && _movementInputDirection < 0)
        {
            Flip();
        }
        else if (!_isFacingRight && _movementInputDirection > 0)
        {
            Flip();
        }

        _isWalking = Mathf.Abs(rb.velocity.x) >= 0.01f;
    }

    private void UpdateAnimations()
    {
        anim.SetBool(IsWalking, _isWalking);
        anim.SetBool(IsGrounded, _isGrounded);
        anim.SetFloat(YVelocity, rb.velocity.y);
        anim.SetBool(IsWallSliding, _isWallSliding);
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isGrounded || (_amountOfJumpsLeft > 0 && _isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                _jumpTimer = jumpTimerSet;
                _isAttemptingToJump = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && _isTouchingWall)
        {
            if (!_isGrounded && _movementInputDirection != _facingDirection)
            {
                _canMove = false;
                _canFlip = false;

                _turnTimer = turnTimerSet;
            }
        }

        if (!_canMove)
        {
            _turnTimer -= Time.deltaTime;

            if (_turnTimer <= 0)
            {
                _canMove = true;
                _canFlip = true;
            }
        }

        if (_checkJumpMultiplier && !Input.GetKey(KeyCode.Space))
        {
            _checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetButtonDown($"Dash"))
        {
            AttemptToDash();
        }
    }

    public int GetFacingDirection()
    {
        return _facingDirection;
    }

    private void CheckDash()
    {
        if (_isDashing)
        {
            if (_dashTimeLeft > 0)
            {
                _canMove = false;
                _canFlip = false;
                rb.velocity = new Vector2(dashSpeed * _facingDirection, 0);
                _dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - _lastImageXpos) > distanceBetweenImages)
                {
                    ObjectPooler.Instance.SpawnFromPool(afterImageTag, transform.position, transform.rotation);
                    _lastImageXpos = transform.position.x;
                }
            }

            if (_dashTimeLeft <= 0 || _isTouchingWall)
            {
                _isDashing = false;
                _canMove = true;
                _canFlip = true;
            }
        }
    }

    private void AttemptToDash()
    {
        _isDashing = true;
        _dashTimeLeft = dashTime;
        _lastDash = Time.time;
        ObjectPooler.Instance.SpawnFromPool(afterImageTag, transform.position, transform.rotation);
        _lastImageXpos = transform.position.x;
    }

    private void CheckJump()
    {
        if (_jumpTimer > 0)
        {
            // WallJump
            if (!_isGrounded && _isTouchingWall && _movementInputDirection != 0 &&
                _movementInputDirection != _facingDirection)
            {
                WallJump();
            }
            else if (_isGrounded)
            {
                NormalJump();
            }
        }

        if (_isAttemptingToJump)
        {
            _jumpTimer -= Time.deltaTime;
        }

        if (_wallJumpTimer > 0)
        {
            if (_hasWallJumped && _movementInputDirection == _lastWallJumpDirection)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                _hasWallJumped = false;
            }
            else if (_wallJumpTimer <= 0)
            {
                _hasWallJumped = false;
            }
            else
            {
                _wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (_canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _amountOfJumpsLeft--;
            _jumpTimer = 0;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        /*else if (_isWallSliding && _movementInputDirection == 0 && _canJump) // wall hop
        {
            _isWallSliding = false;
            _amountOfJumpsLeft--;
            var forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -_facingDirection,
                wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }*/
        if (_canWallJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            _isWallSliding = false;
            _amountOfJumpsLeft = amountOfJumps;
            _amountOfJumpsLeft--;
            var forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * _movementInputDirection,
                wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            _jumpTimer = 0;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
            _turnTimer = 0;
            _canMove = true;
            _canFlip = true;
            _hasWallJumped = true;
            _wallJumpTimer = wallJumpTimerSet;
            _lastWallJumpDirection = -_facingDirection;
        }
    }

    private void ApplyMovement()
    {
        if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (_canMove)
        {
            rb.velocity = new Vector2(movementSpeed * _movementInputDirection, rb.velocity.y);
        }

        if (_isWallSliding)
        {
            if (rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }
    }

    public void DisableFlip()
    {
        _canFlip = false;
    }

    public void EnableFlip()
    {
        _canFlip = true;
    }

    private void Flip()
    {
        if (!_isWallSliding && _canFlip)
        {
            _facingDirection *= -1;
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        var position = wallCheck.position;

        Gizmos.DrawLine(position, new Vector3(position.x + wallCheckDistance, position.y, position.z));
    }
}