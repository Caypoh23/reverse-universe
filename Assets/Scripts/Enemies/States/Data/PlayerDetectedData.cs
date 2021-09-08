using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
    public class PlayerDetectedData : ScriptableObject
    {
        public float longRangeActionTime = 1.5f;
    }
}