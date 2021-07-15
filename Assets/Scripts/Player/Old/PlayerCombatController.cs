using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private bool _gotInput;
    private bool _isAttacking;
    private bool _isFirstAttack;

    [SerializeField] private bool combatEnabled;

    [SerializeField] private float inputTimer;
    [SerializeField] private float attack1Radius;
    [SerializeField] private float attack1Damage;

    [SerializeField] private Transform attack1HitBoxPos;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private Animator anim;

    [SerializeField] private PlayerController PC;

    private float _lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetails = new float[2];

    [SerializeField] private PlayerStats playerStats;

    private static readonly int CanAttack = Animator.StringToHash("canAttack");
    private static readonly int FirstAttack = Animator.StringToHash("firstAttack");
    private static readonly int Attack1 = Animator.StringToHash("attack1");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Start()
    {
        anim.SetBool(CanAttack, combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (combatEnabled)
            {
                _gotInput = true;
                _lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (_gotInput)
        {
            if (!_isAttacking)
            {
                _gotInput = false;
                _isAttacking = true;
                _isFirstAttack = !_isFirstAttack;
                anim.SetBool(Attack1, true);
                anim.SetBool(FirstAttack, _isFirstAttack);
                anim.SetBool(IsAttacking, _isAttacking);
            }
        }

        if (Time.time >= _lastInputTime + inputTimer)
        {
            _gotInput = false;
        }
    }

    private void CheckAttackHitBox()
    {
        var detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius,
            whatIsDamageable);

        attackDetails[0] = attack1Damage;
        attackDetails[1] = transform.position.x;

        foreach (var other in detectedObjects)
        {
            other.transform.parent.SendMessage("Damage", attackDetails);
            // Instantiate hit particle
        }
    }

    private void FinishAttack1()
    {
        _isAttacking = false;
        anim.SetBool(IsAttacking, _isAttacking);
        anim.SetBool(Attack1, false);
    }

    private void Damage(float[] attackDetails)
    {
        if (!PC.GetDashStatus())
        {
            int direction;
            
            playerStats.DecreaseHealth(attackDetails[0]);

            if (attackDetails[1] < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            PC.Knockback(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}