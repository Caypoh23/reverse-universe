﻿using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
    public class D_StunState : ScriptableObject
    {
        public float stunTime = 3f;
        public float stunKnockbackTime = 0.2f;
        public float stunKnockbackSpeed = 20f;

        public Vector2 stunKnockbackAngle;
    }
}