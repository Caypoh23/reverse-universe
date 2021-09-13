using System;
using System.Collections;
using System.Collections.Generic;
using Cores.CoreComponents;
using Player.Input;
using ReverseTime.Commands;
using UnityEngine;

namespace ReverseTime
{
    public class RewindTime : MonoBehaviour
    {
        [SerializeField] private PlayerInputHandler inputHandler;

        private readonly CommandStack _commandStack = new CommandStack();

        private bool _isHolding;

        [SerializeField] private float timerValue = 5.0f;

        [SerializeField] private Movement movement;

        private float _currentTimerValue;
        private bool _canClearCache;


        private void Awake()
        {
            _currentTimerValue = timerValue;
        }

        private void Update()
        {
            ReverseTime();
            StopRevisingTime();

            /*if(_currentTimerValue >= 0)
            {
                _currentTimerValue -= Time.deltaTime;
                
            }

            if (_canClearCache && _currentTimerValue <= 0)
            {
                _commandStack.ClearCache();
                _canClearCache = false;
                _currentTimerValue = timerValue;
            }*/
        }

        private void ReverseTime()
        {
            if (inputHandler.CanReverseTimeInput)
            {
                Debug.Log(inputHandler.CanReverseTimeInput + " Pressed rewind");

                inputHandler.UseTimeReverseInput();
                _isHolding = true;

                _commandStack.UndoLastCommand();
            }
        }

        private void StopRevisingTime()
        {
            if (_isHolding)
            {
                if (inputHandler.CanReverseTimeInputStop)
                {
                    _isHolding = false;

                    Debug.Log("Released rewind");
                }
            }
        }
    }
}