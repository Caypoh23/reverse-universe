using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]
    public class RangedAttackStateData : ScriptableObject
    {
        public GameObject projectile;
        public Tag projectileTag;
        public float projectileDamage = 10f;
        public float projectileSpeed = 12f;
        public float projectileTravelDistance = 5f;
    }
}
