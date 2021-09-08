using UnityEngine;

namespace ScriptableObjects.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public int amountOfAttacks { get; protected set; }
        public float[] movementSpeed { get; protected set; }
    }
}