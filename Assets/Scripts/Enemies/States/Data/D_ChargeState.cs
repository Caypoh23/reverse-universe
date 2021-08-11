﻿using UnityEngine;

namespace Enemies.States.Data
{
    [CreateAssetMenu(fileName = "newChargeStateData", menuName = "Data/State Data/Charge State")]
    public class D_ChargeState : ScriptableObject
    {
        public float chargeSpeed = 6f;

        public float chargeTime = 2f;
    }
}