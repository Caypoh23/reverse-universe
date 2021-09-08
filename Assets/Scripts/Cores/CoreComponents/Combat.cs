using Interfaces;
using UnityEngine;

namespace Cores.CoreComponents
{
    public class Combat : CoreComponent, IDamageable, IKnockbackable
    {
        private bool _isKnockbackActive;
        private float _knockbackStartTime;

        public void LogicUpdate()
        {
            CheckKnockback();
        }

        public void TakeDamage(float amount)
        {
            Debug.Log(Core.transform.parent.name + " Damaged!");
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
            if (_isKnockbackActive && Core.Movement.CurrentVelocity.y <= 0.01f && Core.CollisionSenses.IsGrounded)
            {
                _isKnockbackActive = false;
                Core.Movement.CanSetVelocity = true;
            }
        }
    }
}