using System;
using System.Collections;
using System.Collections.Generic;
using Cores.CoreComponents;
using Interfaces;
using MoreMountains.Feedbacks;
using ObjectPool;
using Player.Data;
using Player.Input;
using Player.PlayerStates.SubStates;
using ReverseTime;
using ReverseTime.Commands;
using UnityEngine;

namespace Player.PlayerFiniteStateMachine
{
    public class PlayerBase : MonoBehaviour
    {
        #region State Variables

        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        public PlayerWallSlideState WallSlideState { get; private set; }
        public PlayerWallJumpState WallJumpState { get; private set; }
        public PlayerDashState DashState { get; private set; }
        public PlayerAttackState PrimaryAttackState { get; private set; }
        public PlayerAttackState SecondaryAttackState { get; private set; }

        [SerializeField]
        private PlayerData playerData;

        [SerializeField]
        private MMFeedbacks landFeedback;

        [SerializeField]
        private MMFeedbacks runFeedback;

        public MMFeedbacks LandFeedback => landFeedback;

        public MMFeedbacks RunFeedback => runFeedback;

        #endregion

        #region Components

        public Core Core { get; private set; }
        public Animator Anim { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public PlayerInventory PlayerInventory { get; private set; }
        public ObjectPooler ObjectPooler { get; private set; }

        public Tag afterImageTag;

        #endregion

        #region Other Variables

        private Vector2 _workspace;

        private readonly int IdleParameterName = Animator.StringToHash("Idle");
        private readonly int MoveParameterName = Animator.StringToHash("Move");
        private readonly int InAirParameterName = Animator.StringToHash("InAir");
        private readonly int LandParameterName = Animator.StringToHash("Land");
        private readonly int WallSlideParameterName = Animator.StringToHash("WallSlide");
        private readonly int AttackParameterName = Animator.StringToHash("Attack");
        private readonly int CanDashParameterName = Animator.StringToHash("CanDash");

        #endregion

        #region Unity Callback Functions

        private void Awake()
        {
            Core = GetComponentInChildren<Core>();

            StateMachine = new PlayerStateMachine();

            IdleState = new PlayerIdleState(this, StateMachine, playerData, IdleParameterName);
            MoveState = new PlayerMoveState(this, StateMachine, playerData, MoveParameterName);
            JumpState = new PlayerJumpState(this, StateMachine, playerData, InAirParameterName);
            InAirState = new PlayerInAirState(this, StateMachine, playerData, InAirParameterName);
            LandState = new PlayerLandState(this, StateMachine, playerData, LandParameterName);
            WallSlideState = new PlayerWallSlideState(
                this,
                StateMachine,
                playerData,
                WallSlideParameterName
            );
            WallJumpState = new PlayerWallJumpState(
                this,
                StateMachine,
                playerData,
                InAirParameterName
            );
            DashState = new PlayerDashState(this, StateMachine, playerData, CanDashParameterName);
            PrimaryAttackState = new PlayerAttackState(
                this,
                StateMachine,
                playerData,
                AttackParameterName
            );
            SecondaryAttackState = new PlayerAttackState(
                this,
                StateMachine,
                playerData,
                AttackParameterName
            );
        }

        private void Start()
        {
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            Rb = GetComponent<Rigidbody2D>();
            ObjectPooler = FindObjectOfType<ObjectPooler>();
            PlayerInventory = GetComponent<PlayerInventory>();

            PrimaryAttackState.SetWeapon(PlayerInventory.weapons[(int)CombatInputs.Primary]);
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            Core.LogicUpdate();

            ResetState();

            StateMachine.CurrentState.LogicUpdate();
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

        private void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();

        #endregion

        #region Other Functions

        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishedTrigger() =>
            StateMachine.CurrentState.AnimationFinishedTrigger();
        #endregion
    }
}
