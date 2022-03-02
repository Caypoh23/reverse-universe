﻿using Cores.CoreComponents;
using Interfaces;
using UnityEngine;

namespace ReverseTime.Commands
{
    public class MoveCommand : ICommand
    {
        #region Player Movement

        private Transform _currentPosition;
        private Vector3 _previousPosition;
        private Vector3 _previousRotation;

        #endregion

        #region Animation

        private Animator _animator;
        private string _previousAnimationName;
        private bool _previousAnimationStatus;

        #endregion

        
        # region Player Health
        private Stats _characterHealthStats;
        private float _previousHealthAmount;
        
        #endregion

        public MoveCommand(Transform currentPosition, Animator animator, Stats characterHealthStats)
        {
            _currentPosition = currentPosition;
            _animator = animator;
            _characterHealthStats = characterHealthStats;
        }

        public void Execute()
        {
            _previousPosition = _currentPosition.position;
            _previousRotation.y = _currentPosition.localRotation.eulerAngles.y;
            _previousHealthAmount = _characterHealthStats.CurrentHealthAmount;
            GetAnimationParameterName();
        }

        private void GetAnimationParameterName()
        {
            foreach (var currentAnimatorParameter in _animator.parameters)
            {
                if (
                    CheckAnimationType(currentAnimatorParameter)
                    && CurrentAnimatorParameterName(currentAnimatorParameter)
                )
                {
                    _previousAnimationName = currentAnimatorParameter.name;
                    _previousAnimationStatus = CurrentAnimatorParameterName(
                        currentAnimatorParameter
                    );
                    break;
                }
            }
        }

        public void Undo()
        {
            _currentPosition.position = _previousPosition;
            _currentPosition.localRotation = Quaternion.Euler(0, _previousRotation.y, 0f);
            _characterHealthStats.CurrentHealthAmount = _previousHealthAmount;
            _animator.SetBool(_previousAnimationName, true);
            DeactivateAnimations();
        }

        private void DeactivateAnimations()
        {
            foreach (var currentAnimation in _animator.parameters)
            {
                if (
                    _previousAnimationName != currentAnimation.name
                    && CheckAnimationType(currentAnimation)
                )
                {
                    _animator.SetBool(currentAnimation.name, false);
                }
            }
        }

        private bool CheckAnimationType(AnimatorControllerParameter currentAnimation) =>
            currentAnimation.type == AnimatorControllerParameterType.Bool;

        private bool CurrentAnimatorParameterName(AnimatorControllerParameter animatorParameter) =>
            _animator.GetBool(animatorParameter.name);
    }
}
