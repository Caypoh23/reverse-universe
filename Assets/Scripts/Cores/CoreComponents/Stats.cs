using Interfaces;
using UnityEngine;

namespace Cores.CoreComponents
{
    public class Stats : CoreComponent, ITakeDamage
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private GameObject character;
         private float _currentHealth;

        protected override void Awake()
        {
            base.Awake();

            _currentHealth = maxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                character.SetActive(false);
                Debug.Log("Health is zero!!");
            }
        }

        public void IncreaseHealth(float amount)
        {
            _currentHealth = Mathf.Clamp
            (
                _currentHealth + amount,
                0,
                maxHealth
            );
        }
    }
}