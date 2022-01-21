using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Camera mainCamera;

        public Vector2 RawMovementInput { get; private set; }
        public Vector2 RawDashDirectionInput { get; private set; }
        public Vector2Int DashDirectionInput { get; private set; }
        public int NormalizedInputX { get; private set; }
        public int NormalizedInputY { get; private set; }
        public bool CanJumpInput { get; private set; }
        public bool CanJumpInputStop { get; private set; }
        public bool CanDashInput { get; private set; }
        public bool CanDashInputStop { get; private set; }
        public bool CanDelayTimeInput { get; private set; }
        public bool CanDelayTimeInputStop { get; private set; }
        public bool CanReverseTimeInput { get; private set; }
        public bool CanReverseTimeInputStop { get; private set; }


        public bool[] AttackInputs { get; private set; }

        [SerializeField] private float inputHoldTime = 0.2f;

        private float _jumpInputStartTime;
        private float _dashInputStartTime;
        private float _timeReverseInputStartTime;
        private float _timeDilationInputStartTime;

        private void Start()
        {
            var count = Enum.GetValues(typeof(CombatInputs)).Length;
            AttackInputs = new bool[count];
        }

        private void Update()
        {
            CheckJumpInputHoldTime();
            //CheckTimeReverseInputHoldTime();
            CheckTimeDilationInputHoldTime();
        }

        public void OnPrimaryAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackInputs[(int) CombatInputs.Primary] = true;
            }

            if (context.canceled)
            {
                AttackInputs[(int) CombatInputs.Primary] = false;
            }
        }

        public void OnSecondaryAttackInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                AttackInputs[(int) CombatInputs.Secondary] = true;
            }

            if (context.canceled)
            {
                AttackInputs[(int) CombatInputs.Secondary] = false;
            }
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            RawMovementInput = context.ReadValue<Vector2>();

            NormalizedInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormalizedInputY = Mathf.RoundToInt(RawMovementInput.y);
        }

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                CanJumpInput = true;
                CanJumpInputStop = false;
                _jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                CanJumpInputStop = true;
            }
        }
        
        public void OnTimeReverseInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                CanReverseTimeInput = true;
                CanReverseTimeInputStop = false;
                _timeReverseInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                CanReverseTimeInputStop = true;
            }
        }
        
        public void OnTimeDilationInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                CanDelayTimeInput = true;
                CanDelayTimeInputStop = false;
                _timeDilationInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                CanDelayTimeInputStop = true;
            }
        }

        
        public void OnDashInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                CanDashInput = true;
                CanDashInputStop = false;
                _dashInputStartTime = Time.time;
            }
            else if (context.canceled)
            {
                CanDashInputStop = true;
                CanDashInput = false;   
            }
        }

        public void OnDashDirectionInput(InputAction.CallbackContext context)
        {
            RawDashDirectionInput = context.ReadValue<Vector2>();

            if (playerInput.currentControlScheme == "Keyboard")
            {
                RawDashDirectionInput = mainCamera.ScreenToWorldPoint((Vector3) RawDashDirectionInput) - transform.position;
            }

            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }

        public void UseJumpInput() => CanJumpInput = false;
        public void UseDashInput() => CanDashInput = false;
        public void UseTimeReverseInput() => CanReverseTimeInput = false;
        public void UseTimeDilationInput() => CanDelayTimeInput = false;
        
        private void CheckJumpInputHoldTime()
        {
            if (Time.time >= _jumpInputStartTime + inputHoldTime)
            {
                CanJumpInput = false;
            }
        }
        
        private void CheckTimeReverseInputHoldTime()
        {
            if (Time.time >= _timeReverseInputStartTime + inputHoldTime)
            {
                CanReverseTimeInput = false;
            }
        }
        private void CheckTimeDilationInputHoldTime()
        {
            if (Time.time >= _timeDilationInputStartTime + inputHoldTime)
            {
                CanDelayTimeInput = false;
            }
        }
    }

    public enum CombatInputs
    {
        Primary,
        Secondary
    }
}