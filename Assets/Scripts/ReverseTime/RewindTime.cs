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
        [SerializeField] private PlayerInputHandler inputHandler;
        [SerializeField] private PlayerData playerData;

        private bool _isRewindingTime;

        public bool IsRewindingTime => _isRewindingTime;

        public void ReverseTime(CommandStack commandStack)
        {
            if (inputHandler.CanReverseTimeInput)
            {
                Debug.Log(inputHandler.CanReverseTimeInput + " Pressed rewind");
                
                _isRewindingTime = true;
                commandStack.UndoLastCommand();
            }
        }

        public void StopRevisingTime()
        {
            if (_isRewindingTime)
            {
                if (inputHandler.CanReverseTimeInputStop)
                {
                    inputHandler.UseTimeReverseInput();
                    _isRewindingTime = false;
                    Debug.Log("Released rewind");
                }
            }
        }
    }
}