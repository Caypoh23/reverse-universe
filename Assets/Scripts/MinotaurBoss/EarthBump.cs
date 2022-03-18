using System.Runtime.InteropServices.WindowsRuntime;
using Cores.CoreComponents;
using Interfaces;
using UnityEngine;
public class EarthBump : MonoBehaviour
{
    [SerializeField]
    private Tag playerStatsTag;
    [SerializeField]
    private float damageAmount = 15;

    [SerializeField]
    private Vector2 knockbackAngle = Vector2.one;
    [SerializeField]
    private float knockbackStrength = 10f;

    private int _facingDirection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.HasTag(playerStatsTag))
        {
            ResetFacingDirection();
            
            var damageable = other.GetComponentInChildren<ITakeDamage>();
            var knockbackable = other.GetComponentInChildren<IKnockbackable>();

            damageable.TakeDamage(damageAmount);

            knockbackable?.Knockback(knockbackAngle, knockbackStrength, _facingDirection);
        }
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
