using Enemies.States.Data;
using Intermediaries;
using UnityEngine;

namespace Enemies.StateMachine
{
    public class Entity : MonoBehaviour
    {
        #region Components

        public Animator Anim { get; private set; }
        public AnimationToStateMachine AnimationToState { get; private set; }
        public int LastDamageDirection { get; private set; }
        public Core Core { get; private set; }

        #endregion

        #region State Machine

        public FiniteStateMachine StateMachine;

        [SerializeField] private EntityData entityData;

        #endregion

        #region Variables and Check Transform

        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;
        [SerializeField] private Transform playerCheck;
        [SerializeField] private Transform groundCheck;

        private float _currentHealth;
        private float _currentStunResistance;
        private float _lastDamageTime;

        private Vector2 _velocityWorkspace;

        protected bool IsStunned;
        protected bool IsDead;
        private readonly int _yVelocity = Animator.StringToHash("yVelocity");

        #endregion

        #region Unity Callback Functions

        public virtual void Awake()
        {
            Core = GetComponentInChildren<Core>();

            _currentHealth = entityData.maxHealth;
            _currentStunResistance = entityData.stunResistance;

            Anim = GetComponent<Animator>();
            AnimationToState = GetComponent<AnimationToStateMachine>();

            StateMachine = new FiniteStateMachine();
        }

        public virtual void Update()
        {
            Core.LogicUpdate();
            
            Anim.SetFloat(_yVelocity, Core.Movement.Rb.velocity.y);

            StateMachine.CurrentState.LogicUpdate();

            if (Time.time >= _lastDamageTime + entityData.stunRecoveryTime)
            {
                ResetStunResistance();
            }
        }

        public void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();
        

        #endregion

        #region Check Functions

        public virtual bool CheckPlayerInMinAgroRange()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minAgroDistance,
                entityData.whatIsPlayer);
        }

        public virtual bool CheckPlayerInMaxAgroRange()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxAgroDistance,
                entityData.whatIsPlayer);
        }

        public virtual bool CheckPlayerInCloseRangeAction()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistance,
                entityData.whatIsPlayer);
        }

        #endregion

        public virtual void DamageHop(float velocity)
        {
            _velocityWorkspace.Set(Core.Movement.Rb.velocity.x, velocity);
            Core.Movement.Rb.velocity = _velocityWorkspace;
        }

        public virtual void ResetStunResistance()
        {
            IsStunned = false;
            _currentStunResistance = entityData.stunResistance;
        }

        public virtual void OnDrawGizmos()
        {
            if (Core != null)
            {
                var position = playerCheck.position;

                Gizmos.DrawLine(wallCheck.position,
                    wallCheck.position +
                    (Vector3) (Vector2.right * Core.Movement.FacingDirection * entityData.wallCheckDistance));
                Gizmos.DrawLine(ledgeCheck.position,
                    ledgeCheck.position + (Vector3) (Vector2.down * entityData.ledgeCheckDistance));
                Gizmos.DrawWireSphere(
                    position + (Vector3) (Vector2.right * entityData.closeRangeActionDistance), 0.2f);
                Gizmos.DrawWireSphere(
                    position + (Vector3) (Vector2.right * entityData.minAgroDistance), 0.2f);
                Gizmos.DrawWireSphere(
                    position + (Vector3) (Vector2.right * entityData.maxAgroDistance), 0.2f);
            }
        }
    }
}