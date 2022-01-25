using Player.Weapons;
using UnityEngine;

namespace Intermediaries
{
    public class WeaponAnimationToWeapon : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private void AnimationFinishTrigger()
        {
            _weapon.AnimationFinishedTrigger();
        }

        private void AnimationStartMovementTrigger()
        {
            _weapon.AnimationStartMovementTrigger();
        }

        private void AnimationStopMovementTrigger()
        {
            _weapon.AnimationStopMovementTrigger();
        }

        private void AnimationTurnOffFlipTrigger()
        {
            _weapon.AnimationTurnOffFlipTrigger();
        }

        private void AnimationTurnOnFlipTrigger()
        {
            _weapon.AnimationTurnOnFlipTrigger();
        }

        private void AnimationActionTrigger()
        {
            _weapon.AnimationActionTrigger();
        }
    }
}