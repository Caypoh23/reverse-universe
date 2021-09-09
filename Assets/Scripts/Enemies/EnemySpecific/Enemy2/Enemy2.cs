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

        public override void Awake()
        {
            base.Awake();

            MoveState = new E2_MoveState(this, StateMachine, "move", moveStateData, this);
            IdleState = new E2_IdleState(this, StateMachine, "idle", idleStateData, this);
            PlayerDetectedState =
                new E2_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedStateData, this);
            MeleeAttackState = new E2_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition,
                meleeAttackStateData, this);
            LookForPlayerState =
                new E2_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
            StunState =
                new E2_StunState(this, StateMachine, "stun", stunStateData, this);
            DeadState =
                new E2_DeadState(this, StateMachine, "dead", deadStateData, this);
            DodgeState =
                new E2_DodgeState(this, StateMachine, "dodge", dodgeStateData, this);
            RangedAttackState =
                new E2_RangedAttackState(this, StateMachine, "rangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        }

        private void Start()
        {
            StateMachine.Initialize(MoveState);
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