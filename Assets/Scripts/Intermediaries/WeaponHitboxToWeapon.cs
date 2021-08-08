using System;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon _weapon;

    private void Awake()
    {
        _weapon = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _weapon.AddToDetected(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _weapon.RemoveFromDetected(other);
    }
}
