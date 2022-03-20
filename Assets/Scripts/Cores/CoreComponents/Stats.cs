using System.Collections.Generic;
using Interfaces;
using ReverseTime;
using ReverseTime.Commands;
using UnityEngine;

namespace Cores.CoreComponents
{
    public class Stats : CoreComponent, ITakeDamage
    {
        [SerializeField]
        private float maxHealth;

        [SerializeField]
        private GameObject character;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private BoxCollider2D boxCollider;

        private float _currentHealth;

        private readonly int DeathParameterName = Animator.StringToHash("Die");

        public float CurrentHealthAmount
        {
            get => _currentHealth;
            set { _currentHealth = value; }
        }
        
        public float MaxHealth
        {
            get => maxHealth;
            private set { maxHealth = value; }
        }

        protected override void Awake() => _currentHealth = maxHealth;

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                //animator.SetBool(DeathParameterName, true);
                Debug.Log("Health is zero!!");
            }
        }

        public void IncreaseHealth()
        {
            _currentHealth = Mathf.Clamp(_currentHealth + 10, 0, maxHealth);
        }
    }
}
