using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.EnemySpecific.Enemy2
{
    public class Enemy2 : Entity
    {
        public E2_MoveState MoveState { get; private set; }
        public E2_IdleState IdleState { get; private set; }
        public E2_PlayerDetectedState PlayerDetectedState { get; private set; }
        public E2_MeleeAttackState MeleeAttackState { get; private set; }
        public E2_LookForPlayerState LookForPlayerState { get; private set; }
        public E2_StunState StunState { get; private set; }
        public E2_DeadState DeadState { get; private set; }
        public E2_DodgeState DodgeState { get; private set; }
        public E2_RangedAttackState RangedAttackState { get; private set; }

        [SerializeField] private MoveStateData moveStateData;
        [SerializeField] private IdleStateData idleStateData;
        [SerializeField] private PlayerDetectedData playerDetectedStateData;
        [SerializeField] private MeleeAttackData meleeAttackStateData;
        [SerializeField] private LookForPlayerData lookForPlayerStateData;
        [SerializeField] private StunStateData stunStateData;
        [SerializeField] private DeadStateData deadStateData;
        [SerializeField] private RangedAttackStateData rangedAttackStateData;
        public DodgeStateData dodgeStateData;

        [SerializeField] private Transform meleeAttackPosition;
        [SerializeField] private Transform rangedAttackPosition;

        #region Animation parameter names

        private readonly int IdleParameterName = Animator.StringToHash("Idle");
        private readonly int MoveParameterName = Animator.StringToHash("Move");
        private readonly int PlayerDetectedParameterName = Animator.StringToHash("PlayerDetected");
        private readonly int MeleeAttackParameterName = Animator.StringToHash("MeleeAttack");
        private readonly int LookForPlayerParameterName = Animator.StringToHash("LookForPlayer");
        private readonly int StunParameterName = Animator.StringToHash("Stun");
        private readonly int DeadParameterName = Animator.StringToHash("Dead");
        private readonly int DodgeParameterName = Animator.StringToHash("Dodge");
        private readonly int RangedAttackParameterName = Animator.StringToHash("RangedAttack");

        #endregion
        
        public override void Awake()
        {
            base.Awake();

            MoveState = new E2_MoveState(this, StateMachine, MoveParameterName, moveStateData, this);
            IdleState = new E2_IdleState(this, StateMachine, IdleParameterName, idleStateData, this);
            PlayerDetectedState =
                new E2_PlayerDetectedState(this, StateMachine, PlayerDetectedParameterName, playerDetectedStateData, this);
            MeleeAttackState = new E2_MeleeAttackState(this, StateMachine, MeleeAttackParameterName, meleeAttackPosition,
                meleeAttackStateData, this);
            LookForPlayerState =
                new E2_LookForPlayerState(this, StateMachine, LookForPlayerParameterName, lookForPlayerStateData, this);
            StunState =
                new E2_StunState(this, StateMachine, StunParameterName, stunStateData, this);
            DeadState =
                new E2_DeadState(this, StateMachine, DeadParameterName, deadStateData, this);
            DodgeState =
                new E2_DodgeState(this, StateMachine, DodgeParameterName, dodgeStateData, this);
            RangedAttackState =
                new E2_RangedAttackState(this, StateMachine, RangedAttackParameterName, rangedAttackPosition, rangedAttackStateData, this);
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }


        public override void DamageHop(float velocity)
        {
            base.DamageHop(velocity);

            if (IsDead)
            {
                StateMachine.ChangeState(DeadState);
            }
            else if (IsStunned && StateMachine.CurrentState != StunState)
            {
                StateMachine.ChangeState(StunState);
            }
            else if (CheckPlayerInMinAgroRange())
            {
                StateMachine.ChangeState(RangedAttackState);
            }
            else if (!CheckPlayerInMinAgroRange())
            {
                LookForPlayerState.SetTurnImmediately(true);
                StateMachine.ChangeState(LookForPlayerState);
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        }
    }
}