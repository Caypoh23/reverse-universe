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

        public MoveCommand(Transform currentPosition, Animator animator)
        {
            _currentPosition = currentPosition;
            _animator = animator;
        }

        public void Execute()
        {
            _previousPosition = _currentPosition.position;
            _previousRotation.y = _currentPosition.localRotation.eulerAngles.y;
            GetAnimationParameterName();
        }

        public void Undo()
        {
            _currentPosition.position = _previousPosition;
            _currentPosition.localRotation = Quaternion.Euler(0, _previousRotation.y, 0f);
            _animator.SetBool(_previousAnimationName, true);
            DeactivateAnimations();
        }

        private void GetAnimationParameterName()
        {
            foreach(var parameter in _animator.parameters)
            {
                if(parameter.type == AnimatorControllerParameterType.Bool && _animator.GetBool(parameter.name))
                {
                   _previousAnimationName = parameter.name;
                   _previousAnimationStatus = _animator.GetBool(parameter.name);
                   break;
                }
            }

            //Debug.Log(_animator.parameters[0].type + " parameter type");

            //_animationName = _playedAnimations.name;
            //Debug.Log(_animator.GetParameter(3).name + " current animation name" + _animator.GetBool("Move") + "Played animation name : " + _animationName);
            //Debug.Log(_previousAnimationName + " previous animation name " + _previousAnimationStatus + " its status");
        }

        private void DeactivateAnimations()
        {
            foreach (var currentAnimation in _animator.parameters)
            {
                if (_previousAnimationName != currentAnimation.name && currentAnimation.type == AnimatorControllerParameterType.Bool)
                {
                    _animator.SetBool(currentAnimation.name, false);
                }
            }
        }
    }
}