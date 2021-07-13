using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    [SerializeField] private GameObject alive;
    [SerializeField] private Rigidbody2D aliveRb;
    [SerializeField] private Animator aliveAnim;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float lastTouchDamageTime;
    [SerializeField] private float touchDamageCooldown;
    [SerializeField] private float touchDamage;
    [SerializeField] private float touchDamageWidth;
    [SerializeField] private float touchDamageHeight;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform touchDamageCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Vector2 knockbackSpeed;
    [SerializeField] private GameObject hitParticle;
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;

    private float _currentHealth;
    private float _knockbackStartTime;
    private float[] _attackDetails = new float[2];

    private int _facingDirection;
    private int _damageDirection;

    private Vector2 _movement;
    private Vector2 _touchDamageBotLeft;
    private Vector2 _touchDamageTopRight;

    private State _currentState;
    private bool _groundDetected;
    private bool _wallDetected;
    private static readonly int Knockback = Animator.StringToHash("knockback");

    private void Start()
    {
        _currentHealth = maxHealth;
        _facingDirection = 1;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }


    // -- WALKING STATE -----------------------------------------

    private void EnterMovingState()
    {
    }

    private void UpdateMovingState()
    {
        _groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        _wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        CheckTouchDamage();
        
        if (!_groundDetected || _wallDetected)
        {
            // Flip
            Flip();
        }
        else
        {
            _movement.Set(movementSpeed * _facingDirection, aliveRb.velocity.y);
            aliveRb.velocity = _movement;
        }
    }

    private void ExitMovingState()
    {
    }

    // -- KNOCKBACK STATE ---------------------------------------------

    private void EnterKnockbackState()
    {
        _knockbackStartTime = Time.time;
        _movement.Set(knockbackSpeed.x * _damageDirection, knockbackSpeed.y);
        aliveRb.velocity = _movement;
        aliveAnim.SetBool(Knockback, true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= _knockbackStartTime + knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool(Knockback, false);
    }


    // -- DEAD STATE --------------------------------------------

    private void EnterDeadState()
    {
        // Spawn chunks and blood
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {
    }

    private void ExitDeadState()
    {
    }

    // -- OTHER FUNCTIONS ----------------------------------------------

    private void Damage(float[] attackDetails)
    {
        _currentHealth -= attackDetails[0];

        Instantiate(hitParticle, alive.transform.position,
            Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > alive.transform.position.x)
        {
            _damageDirection = -1;
        }
        else
        {
            _damageDirection = 1;
        }

        // Hit Particle

        if (_currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (_currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            _touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2),
                touchDamageCheck.position.y - (touchDamageHeight / 2));
            _touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2),
                touchDamageCheck.position.y + (touchDamageHeight / 2));
            var hit = Physics2D.OverlapArea(_touchDamageBotLeft, _touchDamageTopRight, whatIsPlayer);
            
            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                _attackDetails[0] = touchDamage;
                _attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", _attackDetails);
            }
        }
    }

    private void Flip()
    {
        _facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwitchState(State state)
    {
        switch (_currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        _currentState = state;
    }

    private void OnDrawGizmos()
    {
        var groundGizmoPosition = groundCheck.position;
        var wallGizmoPosition = wallCheck.position;
        Gizmos.DrawLine(groundGizmoPosition,
            new Vector2(groundGizmoPosition.x, groundGizmoPosition.y - groundCheckDistance));
        Gizmos.DrawLine(wallGizmoPosition, new Vector2(wallGizmoPosition.x + wallCheckDistance, wallGizmoPosition.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2),
            touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2),
            touchDamageCheck.position.y - (touchDamageHeight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2),
            touchDamageCheck.position.y + (touchDamageHeight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2),
            touchDamageCheck.position.y + (touchDamageHeight / 2));
        
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);
        
    }
}