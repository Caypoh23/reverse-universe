using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData AggressiveWeaponData;

    private List<IDamageable> _detectedDamageable = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            AggressiveWeaponData = (SO_AggressiveWeaponData) weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        var details = AggressiveWeaponData.AttackDetails[AttackCounter];
        
        foreach (var item in _detectedDamageable)
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _detectedDamageable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        var damageable = collision.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _detectedDamageable.Remove(damageable);
        }
    }
}