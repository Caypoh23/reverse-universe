using System;
using Cores.CoreComponents;
using Interfaces;
using Structs;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float gravity;
        [SerializeField] private float damageRadius;

        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask whatIsPlayer;

        [SerializeField] private Transform damagePosition;
        
        [SerializeField] private Tag playerStatsTag;

        
        private WeaponAttackDetails _attackDetails;

        private float _speed;
        private float _travelDistance;
        private float _xStartPos;

        private bool _isGravityOn;
        private bool _hasHitGround;

        private Stats playerStats;

        private void Start() 
        {    
            _xStartPos = transform.position.x;
            playerStats = GameObject.FindGameObjectWithTag(playerStatsTag.name).GetComponent<Stats>();
        }

        private void OnEnable() => ResetGravity();

        private void Update()
        {
            if (!_hasHitGround)
            {
                if (_isGravityOn)
                {
                    var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_hasHitGround)
            {
                var damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
                var groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

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
                playerStats?.TakeDamage(10);
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
                gameObject.SetActive(false);
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
        }

        private void OnDrawGizmos() => Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}