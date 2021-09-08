using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
    public class MeleeAttackData : ScriptableObject
    {
        public float attackRadius = 0.5f;
        public float attackDamage = 10f;

        public Vector2 knockbackAngle = Vector2.one;
        public float knockbackStrength = 10f;

        public LayerMask whatIsPlayer;
    }
}