using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newDeadStateData", menuName = "Data/State Data/Dead State")]
    public class D_DeadState : ScriptableObject
    {
        public GameObject deathChunkParticle;
        public GameObject deathBloodParticle;
    }
}