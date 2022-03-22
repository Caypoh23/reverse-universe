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

        [SerializeField]
        private IdleStateData idleStateData;
        [SerializeField]
        private MoveStateData moveStateData;
        [SerializeField]
        private PlayerDetectedData playerDetectedData;
        [SerializeField]
        private ChargeStateData chargeStateData;
        [SerializeField]
        private LookForPlayerData lookForPlayerStateData;
        [SerializeField]
        private MeleeAttackData meleeAttackStateData;
        [SerializeField]
        private StunStateData stunStateData;
        [SerializeField]
        private DeadStateData deadStateData;

        [SerializeField]
        private Transform meleeAttackPosition;

        #region Animation parameter names

        private readonly int IdleParameterName = Animator.StringToHash("Idle");
        private readonly int MoveParameterName = Animator.StringToHash("Move");
        private readonly int PlayerDetectedParameterName = Animator.StringToHash("PlayerDetected");
        private readonly int MeleeAttackParameterName = Animator.StringToHash("MeleeAttack");
        private readonly int LookForPlayerParameterName = Animator.StringToHash("LookForPlayer");
        private readonly int StunParameterName = Animator.StringToHash("Stun");
        private readonly int DeadParameterName = Animator.StringToHash("Dead");
        private readonly int ChargeParameterName = Animator.StringToHash("Charge");

        #endregion

        public override void Awake()
        {
            base.Awake();

            MoveState = new E1_MoveState(
                this,
                StateMachine,
                MoveParameterName,
                moveStateData,
                this
            );
            IdleState = new E1_IdleState(
                this,
                StateMachine,
                IdleParameterName,
                idleStateData,
                this
            );
            PlayerDetectedState = new E1_PlayerDetectedState(
                this,
                StateMachine,
                PlayerDetectedParameterName,
                playerDetectedData,
                this
            );
            ChargeState = new E1_ChargeState(
                this,
                StateMachine,
                ChargeParameterName,
                chargeStateData,
                this
            );
            LookForPlayerState = new E1_LookForPlayerState(
                this,
                StateMachine,
                LookForPlayerParameterName,
                lookForPlayerStateData,
                this
            );
            MeleeAttackState = new E1_MeleeAttackState(
                this,
                StateMachine,
                MeleeAttackParameterName,
                meleeAttackPosition,
                meleeAttackStateData,
                this
            );
            StunState = new E1_StunState(
                this,
                StateMachine,
                StunParameterName,
                stunStateData,
                this
            );
            DeadState = new E1_DeadState(
                this,
                StateMachine,
                DeadParameterName,
                deadStateData,
                this
            );
        }

        private void Start()
        {
            StateMachine.Initialize(IdleState);
        }

        public override void Update()
        {
            base.Update();

            TimeIsRewinding();

            ResetState();

            CheckIfDead();
        }

        private void ResetState()
        {
            if (Core.Movement.RewindingTimeIsFinished)
            {
                ResetAnimations();
                StateMachine.ChangeState(IdleState);
            }
        }

        private void ResetAnimations()
        {
            foreach (var currentAnimation in Anim.parameters)
            {
                if (CheckAnimationType(currentAnimation))
                {
                    Anim.SetBool(currentAnimation.name, false);
                }
            }
        }

        private bool CheckAnimationType(AnimatorControllerParameter currentAnimation) =>
            currentAnimation.type == AnimatorControllerParameterType.Bool;

        private void CheckIfDead()
        {
            if (Core.Stats.CurrentHealthAmount <= 0)
            {
                StateMachine.ChangeState(DeadState);
            }
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        }
    }
}
