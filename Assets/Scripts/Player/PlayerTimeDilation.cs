﻿using System;
using Player.Data;
using Player.Input;
using Player.PlayerFiniteStateMachine;
using UnityEngine;

namespace Player
{
    public class PlayerTimeDilation : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerData playerData;

        private float _startTime;

        private bool _isHolding;

        private void Awake()
        {
            _startTime = Time.time;
        }

        public void Update()
        {
            if (inputHandler.CanDelayTimeInput)
            {
                Debug.Log(inputHandler.CanDelayTimeInput + " pressed");
                SlowdownTime();
            }

            if (_isHolding)
            {
                if (inputHandler.CanDelayTimeInputStop ||
                    Time.unscaledTime >= _startTime + playerData.maxTimeDilationHoldTime)
                {
                    ResetTimeScale();
                }
            }
        }

        private void SlowdownTime()
        {
            inputHandler.UseTimeDilationInput();
            _isHolding = true;

            Time.timeScale = playerData.timeDilationTimeScale;
            // smooth time dilation
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            _startTime = Time.unscaledTime;
        }

        private void ResetTimeScale()
        {
            _isHolding = false;
            Time.timeScale = 1f;
            Debug.Log("Released");
        }
    }
}