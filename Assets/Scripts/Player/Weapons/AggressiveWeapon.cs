using System.Collections.Generic;
using System.Linq;
using Interfaces;
using ScriptableObjects.Weapons;
using UnityEngine;

namespace Player.Weapons
{
    public class AggressiveWeapon : Weapon
    {
        protected AggressiveWeaponData AggressiveWeaponData;

        private List<IDamageable> _detectedDamageables = new List<IDamageable>();
        private List<IKnockbackable> _detectedKnockbackables = new List<IKnockbackable>();
        protected override void Awake()
        {
            base.Awake();

            if (weaponData.GetType() == typeof(AggressiveWeaponData))
            {
                AggressiveWeaponData = (AggressiveWeaponData) weaponData;
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
        
            foreach (var item in _detectedDamageables.ToList())
            {
                item.TakeDamage(details.damageAmount);
            }
        
            foreach (var item in _detectedKnockbackables.ToList())
            {
                item.Knockback(details.knockbackAngle, details.knockbackStrength, Core.Movement.FacingDirection);
            }
        
        }

        public void AddToDetected(Collider2D collision)
        {
            var damageable = collision.GetComponent<IDamageable>();
            var knockbackable = collision.GetComponent<IKnockbackable>();

            if (damageable != null)
            {
                _detectedDamageables.Add(damageable);
            } 
        
            if (knockbackable != null)
            {
                _detectedKnockbackables.Add(knockbackable);
            }
        }

        public void RemoveFromDetected(Collider2D collision)
        {
            var damageable = collision.GetComponent<IDamageable>();
            var knockbackable = collision.GetComponent<IKnockbackable>();

            if (damageable != null)
            {
                _detectedDamageables.Remove(damageable);
            }
        
            if (knockbackable != null)
            {
                _detectedKnockbackables.Remove(knockbackable);
            }
        }
    }
}