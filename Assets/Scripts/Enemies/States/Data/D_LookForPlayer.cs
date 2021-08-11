using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player State")]
    public class D_LookForPlayer : ScriptableObject
    {
        public int amountOfTurns = 2;
        public float timeBetweenTurns = 0.75f;
    }
}