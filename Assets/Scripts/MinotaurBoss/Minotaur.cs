using System.Collections;
using System.Collections.Generic;
using Enemies.StateMachine;
using Enemies.States.Data;
using MoreMountains.Feedbacks;
using UnityEngine;

public class Minotaur : Entity
{
    public MinotaurIdleState IdleState { get; private set; }
    public MinotaurMoveState MoveState { get; private set; }
    public MinotaurChargeState ChargeState { get; private set; }
    public MinotaurPoundAttackState PoundAttackState { get; private set; }
    public MinotaurSwingAttackState SwingAttackState { get; private set; }

    public MinotaurStompState StompState { get; private set; }

    public MinotaurStunState StunState { get; set; }

    public MinotaurPlayerDetectedState PlayerDetectedState { get; private set; }
    public MinotaurLookForPlayer LookForPlayerState { get; private set; }
    public MinotaurDeadState DeadState { get; private set; }

    public MinotaurIntroState IntroState { get; private set; }

    [SerializeField]
    private IdleStateData idleStateData;
    [SerializeField]
    private MoveStateData moveStateData;
    [SerializeField]
    private ChargeStateData chargeStateData;
    [SerializeField]
    private MeleeAttackData poundAttackStateData;

    [SerializeField]
    private MeleeAttackData swingAttackStateData;

    [SerializeField]
    private DeadStateData deadStateData;
    [SerializeField]
    private PlayerDetectedData playerDetectedStateData;
    [SerializeField]
    private LookForPlayerData lookForPlayerStateData;

    [SerializeField]
    private BossIntroStateData bossIntroStateData;

    [SerializeField]
    private StunStateData stunStateData;

    [SerializeField]
    private MinotaurStompStateData stompStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    [SerializeField]
    private Transform earthBumpSpawnPosition;

    [SerializeField]
    private GameObject bigExplosion;

    [SerializeField]
    private MMFeedbacks landFeedback;

    [SerializeField]
    private BoxCollider2D chargeCollider;

    public MMFeedbacks LandFeedback => landFeedback;

    public Transform EarthBumpSpawnPosition => earthBumpSpawnPosition;

    #region Animation parameter names

    private readonly int IdleParameterName = Animator.StringToHash("Idle");
    private readonly int MoveParameterName = Animator.StringToHash("Move");
    private readonly int PlayerDetectedParameterName = Animator.StringToHash("PlayerDetected");
    private readonly int MeleeAttackParameterName = Animator.StringToHash("MeleeAttack");
    private readonly int SwingParameterName = Animator.StringToHash("SwingAttack");
    private readonly int StompParameterName = Animator.StringToHash("Stomp");
    private readonly int LookForPlayerParameterName = Animator.StringToHash("LookForPlayer");
    private readonly int StunParameterName = Animator.StringToHash("Stun");
    private readonly int DeadParameterName = Animator.StringToHash("Dead");
    private readonly int ChargeParameterName = Animator.StringToHash("Charge");
    private readonly int IntroParameterName = Animator.StringToHash("Intro");

    #endregion

    private int _randomAttackNumber;

    private float _secondPhaseHealthPercentage => Core.Stats.MaxHealth * 0.3f;

    public override void Awake()
    {
        base.Awake();

        MoveState = new MinotaurMoveState(
            this,
            StateMachine,
            MoveParameterName,
            moveStateData,
            this
        );
        IdleState = new MinotaurIdleState(
            this,
            StateMachine,
            IdleParameterName,
            idleStateData,
            this
        );
        PlayerDetectedState = new MinotaurPlayerDetectedState(
            this,
            StateMachine,
            PlayerDetectedParameterName,
            playerDetectedStateData,
            this
        );
        ChargeState = new MinotaurChargeState(
            this,
            StateMachine,
            ChargeParameterName,
            chargeStateData,
            this
        );
        PoundAttackState = new MinotaurPoundAttackState(
            this,
            StateMachine,
            MeleeAttackParameterName,
            meleeAttackPosition,
            poundAttackStateData,
            this
        );
        SwingAttackState = new MinotaurSwingAttackState(
            this,
            StateMachine,
            SwingParameterName,
            meleeAttackPosition,
            swingAttackStateData,
            this
        );
        StompState = new MinotaurStompState(
            this,
            StateMachine,
            StompParameterName,
            meleeAttackPosition,
            stompStateData,
            this
        );
        DeadState = new MinotaurDeadState(
            this,
            StateMachine,
            DeadParameterName,
            deadStateData,
            this
        );
        IntroState = new MinotaurIntroState(
            this,
            StateMachine,
            IntroParameterName,
            bossIntroStateData,
            this
        );
        LookForPlayerState = new MinotaurLookForPlayer(
            this,
            StateMachine,
            LookForPlayerParameterName,
            lookForPlayerStateData,
            this
        );
        StunState = new MinotaurStunState(
            this,
            StateMachine,
            StunParameterName,
            stunStateData,
            this
        );
    }

    private void Start()
    {
        StateMachine.Initialize(IntroState);
    }

    public override void Update()
    {
        base.Update();

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

    public bool IsInSecondPhase()
    {
        if (Core.Stats.CurrentHealthAmount <= _secondPhaseHealthPercentage)
        {
            return true;
        }

        return false;
    }

    public int GenerateRandomNumber() => _randomAttackNumber = Random.Range(0, 100);

    public void ActivateExplosion() => bigExplosion.SetActive(true);

    public void DeactivateExplosion() => bigExplosion.SetActive(false);

    public void ActivateChargeCollider() => chargeCollider.enabled = true;

    public void DeactivateChargeCollider() => chargeCollider.enabled = false;

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, poundAttackStateData.attackRadius);
    }
}
