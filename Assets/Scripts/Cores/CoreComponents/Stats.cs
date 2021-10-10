using UnityEngine;

namespace Cores.CoreComponents
{
    public class Stats : CoreComponent
    {
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        protected override void Awake()
        {
            base.Awake();

            _currentHealth = maxHealth;
        }

        public void DecreaseHealth(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
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