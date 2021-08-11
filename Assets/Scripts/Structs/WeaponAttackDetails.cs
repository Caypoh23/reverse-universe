using System;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public struct WeaponAttackDetails
    {
        public string attackName;
        public float movementSpeed;
        public float damageAmount;

        public float knockbackStrength;
        public Vector2 knockbackAngle;
    }
}