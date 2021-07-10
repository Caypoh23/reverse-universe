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

    private float _lastInputTime = Mathf.NegativeInfinity;

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
        foreach (var other in detectedObjects)
        {
            other.transform.parent.SendMessage("Damage", attack1Damage);
            // Instantiate hit particle
        }
    }

    private void FinishAttack1()
    {
        _isAttacking = false;
        anim.SetBool(IsAttacking, _isAttacking);
        anim.SetBool(Attack1, false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}