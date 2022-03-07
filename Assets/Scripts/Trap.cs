using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Tag playerTag;
    [SerializeField] private float damageAmount = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.HasTag(playerTag))
            return;

        var damageable = other.GetComponent<ITakeDamage>();
        damageable.TakeDamage(damageAmount);
    }
}
