using UnityEngine;

namespace ScriptableObjects
{
        [CreateAssetMenu(fileName = "HeroData", menuName = "Data/Hero Data/Base Data")]
        public class PlayerData : ScriptableObject
        {
                [Header("General")]
                public float MaxHealth = 100f;

                [Header("Move State")]
                public float MovementSpeed = 10f;

                [Header("Attack State")]
                public float DamageAmount = 10;
                public float AttackCooldown = 0.5f;
        }
}
