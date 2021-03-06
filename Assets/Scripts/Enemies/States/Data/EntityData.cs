using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
    public class EntityData : ScriptableObject
    {
        public float maxHealth = 30;

        public float damageHopSpeed = 3f;

        public float wallCheckDistance = 0.2f;
        public float ledgeCheckDistance = 0.4f;
        public float groundCheckRadius = 0.4f;

        public float stunResistance = 3f;
        public float stunRecoveryTime = 2f;

        public float minAgroDistance = 3f;
        public float maxAgroDistance = 4f;

        public float closeRangeActionDistance = 1f;
        public float touchingRangeActionDistance = 1f;

        public GameObject hitParticle;

        public LayerMask whatIsGround;
        public LayerMask whatIsPlayer;
    }
}