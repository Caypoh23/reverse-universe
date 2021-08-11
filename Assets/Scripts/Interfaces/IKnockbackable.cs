using UnityEngine;

namespace Interfaces
{
    public interface IKnockbackable
    {
        void Knockback(Vector2 angle, float strength, int direction);
    }
}
