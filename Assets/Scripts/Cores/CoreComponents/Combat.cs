using Interfaces;
using UnityEngine;
using DG.Tweening;

namespace Cores.CoreComponents
{
    public class Combat : CoreComponent, ITakeDamage, IKnockbackable
    {
        [SerializeField]
        private float maxKnockbackTime = 0.2f;
        [SerializeField]
        private Material hitMaterial;
        
        private float _fadeMultiplier = .12f;

        private bool _isKnockbackActive;
        private float _knockbackStartTime;

        private bool _canResetHitMaterial;

        private const string HitMaterialName = "_HitEffectBlend";

        protected override void Awake()
        {
            base.Awake();

            ResetHitMaterial();
        }

        public override void LogicUpdate()
        {
            CheckKnockback();

            if (_canResetHitMaterial)
            {
                ResetHitMaterial();
            }
        }

        public void TakeDamage(float amount)
        {
            Debug.Log(Core.transform.parent.name + " Damaged!");
            Core.Stats.TakeDamage(amount);
            ActivateHitMaterial();
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            Core.Movement.SetVelocity(strength, angle, direction);
            Core.Movement.CanSetVelocity = false;
            _isKnockbackActive = true;
            _knockbackStartTime = Time.time;
        }

        private void CheckKnockback()
        {
            if (
                _isKnockbackActive
                && Core.Movement.CurrentVelocity.y <= 0.01f
                && (
                    Core.CollisionSenses.IsGrounded
                    || Time.time >= _knockbackStartTime + maxKnockbackTime
                )
            )
            {
                _isKnockbackActive = false;
                Core.Movement.CanSetVelocity = true;
                Core.Movement.SetVelocityX(0);
            }
        }

        private void ActivateHitMaterial() =>
            hitMaterial
                .DOFloat(1f, HitMaterialName, _fadeMultiplier)
                .OnComplete(
                    () =>
                    {
                        _canResetHitMaterial = true;
                    }
                );

        private void ResetHitMaterial()
        {
            hitMaterial
                .DOFloat(0f, HitMaterialName, _fadeMultiplier)
                .OnComplete(
                    () =>
                    {
                        _canResetHitMaterial = false;
                    }
                );
            ;
        }
    }
}
