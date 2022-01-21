using Enemies.StateMachine;
using Enemies.States.Data;
using UnityEngine;

namespace Enemies.EnemySpecific.Enemy1
{
    public class Enemy1 : Entity
    {
        public E1_IdleState IdleState { get; private set; }
        public E1_MoveState MoveState { get; private set; }
        public E1_PlayerDetectedState PlayerDetectedState { get; private set; }
        public E1_ChargeState ChargeState { get; private set; }
        public E1_LookForPlayerState LookForPlayerState { get; private set; }
        public E1_MeleeAttackState MeleeAttackState { get; private set; }
        public E1_StunState StunState { get; private set; }
        public E1_DeadState DeadState { get; private set; }

        [SerializeField] private IdleStateData idleStateData;
        [SerializeField] private MoveStateData moveStateData;
        [SerializeField] private PlayerDetectedData playerDetectedData;
        [SerializeField] private ChargeStateData chargeStateData;
        [SerializeField] private LookForPlayerData lookForPlayerStateData;
        [SerializeField] private MeleeAttackData meleeAttackStateData;
        [SerializeField] private StunStateData stunStateData;
        [SerializeField] private DeadStateData deadStateData;


        [SerializeField] private Transform meleeAttackPosition;


        public override void Awake()
        {
            base.Awake();

            MoveState = new E1_MoveState(this, StateMachine, "move", moveStateData, this);
            IdleState = new E1_IdleState(this, StateMachine, "idle", idleStateData, this);
            PlayerDetectedState =
                new E1_PlayerDetectedState(this, StateMachine, "playerDetected", playerDetectedData, this);
            ChargeState = new E1_ChargeState(this, StateMachine, "charge", chargeStateData, this);
            LookForPlayerState =
                new E1_LookForPlayerState(this, StateMachine, "lookForPlayer", lookForPlayerStateData, this);
            MeleeAttackState =
                new E1_MeleeAttackState(this, StateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData,
                    this);
            StunState = new E1_StunState(this, StateMachine, "stun", stunStateData, this);
            DeadState = new E1_DeadState(this, StateMachine, "dead", deadStateData, this);
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        }
    }
}