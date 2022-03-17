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

    public MinotaurPlayerDetectedState PlayerDetectedState { get; private set; }
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
    private BossIntroStateData bossIntroStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    [SerializeField]
    private GameObject bigExplosion;

    [SerializeField]
    private MMFeedbacks landFeedback;

    public MMFeedbacks LandFeedback => landFeedback;

    #region Animation parameter names

    private readonly int IdleParameterName = Animator.StringToHash("Idle");
    private readonly int MoveParameterName = Animator.StringToHash("Move");
    private readonly int PlayerDetectedParameterName = Animator.StringToHash("PlayerDetected");
    private readonly int MeleeAttackParameterName = Animator.StringToHash("MeleeAttack");
    private readonly int SwingParameterName = Animator.StringToHash("SwingAttack");
    private readonly int LookForPlayerParameterName = Animator.StringToHash("LookForPlayer");
    private readonly int StunParameterName = Animator.StringToHash("Stun");
    private readonly int DeadParameterName = Animator.StringToHash("Dead");
    private readonly int ChargeParameterName = Animator.StringToHash("Charge");
    private readonly int IntroParameterName = Animator.StringToHash("Intro");

    #endregion

    private int _randomAttackNumber;

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
    }

    private void Start()
    {
        StateMachine.Initialize(IntroState);
    }

    public int GenerateRandomNumber() => _randomAttackNumber = Random.Range(0, 100);

    public void ActivateExplosion() => bigExplosion.SetActive(true);

    public void DeactivateExplosion() => bigExplosion.SetActive(false);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, poundAttackStateData.attackRadius);
    }
}
