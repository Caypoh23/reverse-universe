using System;
using System.Collections;
using System.Collections.Generic;
using Cores.CoreComponents;
using Player.Data;
using Player.Input;
using ReverseTime.Commands;
using UnityEngine;

namespace ReverseTime
{
    public class RewindTime : MonoBehaviour
    {
        [SerializeField]
        private PlayerInputHandler inputHandler;
        [SerializeField]
        private float maxReverseTimerAmount = 5;
        [SerializeField]
        private int amountOfReverseTime = 3;

        [SerializeField]
        private TimerSlider timerSlider;

        [SerializeField]
        private RewindTimeCounterUI rewindTimeCounterUI;

        private float _currentReverseTimerAmount;
        private int _currentAmountOfReverseTime;

        private bool _isRewindingTime;
        private const float ResetTimerMultiplier = 1;

        public bool IsRewindingTime => _isRewindingTime;

        public bool RewindingTimeIsFinished { get; private set; }

        public float _CurrentAmountOfReverseTime => _currentAmountOfReverseTime;

        private float _rewindTimeCooldown = 0.1f;

        private void Awake()
        {
            _currentAmountOfReverseTime = amountOfReverseTime;
        }

        private void Update()
        {
            StartReverseTimer();
            ResetReverseTimer();
            StopRevisingTime();
        }

        private void StartReverseTimer()
        {
            if (_currentReverseTimerAmount >= 0 && _isRewindingTime)
            {
                _currentReverseTimerAmount -= Time.deltaTime;
                timerSlider.UpdateTimerSlider(_currentReverseTimerAmount);
            }
        }

        private void ResetReverseTimer()
        {
            if (RewindingTimeIsFinished && _rewindTimeCooldown >= 0)
            {
                _rewindTimeCooldown -= Time.deltaTime;
            }
            else if (_rewindTimeCooldown <= 0)
            {
                RewindingTimeIsFinished = false;
                _rewindTimeCooldown = 0.1f;
            }

            if (!_isRewindingTime && _currentReverseTimerAmount < maxReverseTimerAmount)
            {
                _currentReverseTimerAmount += Time.deltaTime;
                timerSlider.UpdateTimerSlider(_currentReverseTimerAmount);
            }
        }

        public void ReverseTime(CommandStack commandStack)
        {
            if (inputHandler.CanReverseTimeInput && _currentAmountOfReverseTime > 0)
            {
                //Debug.Log(inputHandler.CanReverseTimeInput + " Pressed rewind");

                _isRewindingTime = true;
                commandStack.UndoLastCommand();
            }
        }

        public void StopRevisingTime()
        {
            if (_isRewindingTime)
            {
                if (inputHandler.CanReverseTimeInputStop || _currentReverseTimerAmount < 0)
                {
                    RewindingTimeIsFinished = true;
                    inputHandler.UseTimeReverseInput();
                    _isRewindingTime = false;
                    _currentAmountOfReverseTime--;
                    rewindTimeCounterUI.PlayDeactivateAnimation(_currentAmountOfReverseTime);
                    //Debug.Log("Released rewind");
                }
            }
        }
    }
}
