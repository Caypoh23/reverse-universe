using Interfaces;
using UnityEngine;

namespace Enemies
{
    public class CombatTestDummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject hitParticles;
        private readonly int _damage = Animator.StringToHash("damage");

        public void TakeDamage(float amount)
        {
            Debug.Log(amount + " damage taken");

            Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            anim.SetTrigger(_damage);
        }
    }
}