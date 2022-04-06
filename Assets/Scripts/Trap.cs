using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private Tag playerTag;

    [SerializeField]
    private Vector2 knockbackAngle = Vector2.one;
    [SerializeField]
    private float knockbackStrength = 10f;

    [SerializeField]
    private float damageAmount = 5f;

    private int _facingDirection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.HasTag(playerTag))
            return;

        var knockbackable = other.GetComponentInChildren<IKnockbackable>();

        var damageable = other.GetComponentInChildren<ITakeDamage>();

        damageable.TakeDamage(damageAmount);

        knockbackable?.Knockback(knockbackAngle, knockbackStrength, _facingDirection);
    }

    private void ResetFacingDirection()
    {
        if (transform.parent.eulerAngles.y >= 180f)
        {
            _facingDirection = -1;
        }
        else
        {
            _facingDirection = 1;
        }
    }
}
