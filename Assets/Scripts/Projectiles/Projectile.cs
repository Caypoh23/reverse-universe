using System;
using Cores.CoreComponents;
using Interfaces;
using Structs;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float gravity;
        [SerializeField]
        private float damageRadius;
        [SerializeField]
        private float damageAmount = 10f;
        [SerializeField]
        private float maxAliveTime = 2f;

        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private LayerMask whatIsPlayer;

        [SerializeField]
        private Transform damagePosition;

        [SerializeField]
        private Tag playerStatsTag;

        [SerializeField]
        private Tag playerWeaponTag;

        [SerializeField]
        private ParticleSystem brokenParticles;

        private WeaponAttackDetails _attackDetails;

        private float _speed;
        private float _travelDistance;
        private float _xStartPos;
        private float _currentAliveTime;

        private bool _isGravityOn;
        private bool _hasHitGround;

        private Stats playerStats;

        private void OnEnable() => _currentAliveTime = maxAliveTime;

        private void Start()
        {
            _xStartPos = transform.position.x;
            playerStats = GameObject
                .FindGameObjectWithTag(playerStatsTag.name)
                .GetComponent<Stats>();
        }

        private void Update()
        {
            if (!_hasHitGround)
            {
                RotateProjectile();
            }
            else
            {
                DeactivateProjectileWithDelay();
            }
        }

        private void RotateProjectile()
        {
            if (_isGravityOn)
            {
                var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void DeactivateProjectileWithDelay()
        {
            if (_currentAliveTime >= 0)
            {
                _currentAliveTime -= Time.deltaTime;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            if (!_hasHitGround)
            {
                var damageHit = Physics2D.OverlapCircle(
                    damagePosition.position,
                    damageRadius,
                    whatIsPlayer
                );
                var groundHit = Physics2D.OverlapCircle(
                    damagePosition.position,
                    damageRadius,
                    whatIsGround
                );

                _rb.velocity = transform.right * _speed;

                CheckDamageHit(damageHit);

                CheckGroundHit(groundHit);

                ActivateGravity();
            }
        }

        private void CheckDamageHit(bool damageHit)
        {
            if (damageHit)
            {
                playerStats?.TakeDamage(damageAmount);
                gameObject.SetActive(false);
            }
        }

        private void CheckGroundHit(bool groundHit)
        {
            if (groundHit)
            {
                _hasHitGround = true;
                _rb.gravityScale = 0.0f;
                _rb.velocity = Vector2.zero;
            }
        }

        private void ActivateGravity()
        {
            if (Mathf.Abs((_xStartPos) - transform.position.x) >= _travelDistance && !_isGravityOn)
            {
                _isGravityOn = true;
                _rb.gravityScale = gravity;
            }
        }

        private void ResetGravity()
        {
            _isGravityOn = false;
            _hasHitGround = false;

            _rb.gravityScale = 0.0f;
        }

        public void FireProjectile(float speed, float travelDistance, float damage)
        {
            _speed = speed;
            _travelDistance = travelDistance;
            _attackDetails.damageAmount = damage;

            ResetGravity();
        }

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
