using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;
    
    protected Animator BaseAnimator;
    protected Animator WeaponAnimator;
    
    protected PlayerAttackState State;
    protected Core Core;
    protected int AttackCounter;
    
    private readonly int _attack = Animator.StringToHash("attack");
    private readonly int _attackCounter = Animator.StringToHash("attackCounter");

    protected virtual void Awake()
    {
        BaseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        
        gameObject.SetActive(false);
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (AttackCounter >= weaponData.amountOfAttacks)
        {
            AttackCounter = 0;
        }
        
        BaseAnimator.SetBool(_attack, true);
        WeaponAnimator.SetBool(_attack, true);
        
        BaseAnimator.SetInteger(_attackCounter, AttackCounter);
        WeaponAnimator.SetInteger(_attackCounter, AttackCounter);
    }

    public virtual void ExitWeapon()
    {
        BaseAnimator.SetBool(_attack, false);
        WeaponAnimator.SetBool(_attack, false);

        AttackCounter++;
        
        gameObject.SetActive(false);
    }

    #region Animation Triggers

    public virtual void AnimationFinishedTrigger()
    {
        State.AnimationFinishedTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        State.SetPlayerVelocity(weaponData.movementSpeed[AttackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        State.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        State.SetFlipCheck(false);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        State.SetFlipCheck(true);
    }

    public virtual void AnimationActionTrigger()
    {
        
    }

    #endregion

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        State = state;
        Core = core;
    }
}