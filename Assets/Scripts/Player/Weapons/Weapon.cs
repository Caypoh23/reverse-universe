using Player.PlayerStates.SubStates;
using ScriptableObjects.Weapons;
using UnityEngine;

namespace Player.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;
        [SerializeField] private Animator baseWeaponAnimator;
        [SerializeField] private Animator weaponAnimator;
        
        protected PlayerAttackState State;
        protected Core Core;
        protected int AttackCounter;

        private readonly int _attack = Animator.StringToHash("Attack");
        private readonly int _attackCounter = Animator.StringToHash("AttackCounter");

        protected virtual void Awake()
        {
            gameObject.SetActive(false);
        }

        public virtual void EnterWeapon()
        {
            gameObject.SetActive(true);

            if (AttackCounter >= weaponData.amountOfAttacks)
            {
                AttackCounter = 0;
            }

            baseWeaponAnimator.SetBool(_attack, true);
            weaponAnimator.SetBool(_attack, true);

            baseWeaponAnimator.SetInteger(_attackCounter, AttackCounter);
            weaponAnimator.SetInteger(_attackCounter, AttackCounter);
        }

        public virtual void ExitWeapon()
        {
            baseWeaponAnimator.SetBool(_attack, false);
            weaponAnimator.SetBool(_attack, false);

            AttackCounter++;

            gameObject.SetActive(false);
        }

        #region Animation Triggers

        public virtual void AnimationFinishedTrigger()
        {
            State.AnimationFinishedTrigger();
        }

        public virtual void AnimationStartMovementTrigger()
        {
            State.SetPlayerVelocity(weaponData.movementSpeed[AttackCounter]);
        }

        public virtual void AnimationStopMovementTrigger()
        {
            State.SetPlayerVelocity(0f);
        }

        public virtual void AnimationTurnOffFlipTrigger()
        {
            State.SetFlipCheck(false);
        }

        public virtual void AnimationTurnOnFlipTrigger()
        {
            State.SetFlipCheck(true);
        }

        public virtual void AnimationActionTrigger()
        {
        }

        #endregion

        public void InitializeWeapon(PlayerAttackState state, Core core)
        {
            State = state;
            Core = core;
        }
    }
}