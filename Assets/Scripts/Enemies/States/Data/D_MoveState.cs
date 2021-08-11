using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move State")]
    public class D_MoveState : ScriptableObject
    {
        // Data container to save large amounts of data independent of class instances
        public float movementSpeed = 3f;
    }
}