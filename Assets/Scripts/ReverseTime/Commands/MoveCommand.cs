using Interfaces;
using UnityEngine;

namespace ReverseTime.Commands
{
    public class MoveCommand : ICommand
    {
        private Transform _currentPosition;
        private Vector3 _previousPosition;
        private Vector3 _previousRotation;

        private Animator _animator;

        private string _animationName;

        private AnimatorControllerParameter _playedAnimations;

        public MoveCommand(Transform currentPosition, Animator animator)
        {
            _currentPosition = currentPosition;
            _animator = animator;
        }

        public void Execute()
        {
            _previousPosition = _currentPosition.position;
            _previousRotation.y = _currentPosition.localRotation.eulerAngles.y;
            //GetAnimationParameterName();
        }

        public void Undo()
        {
            _currentPosition.position = _previousPosition;
            _currentPosition.localRotation = Quaternion.Euler(0, _previousRotation.y, 0f);
            //_animator.SetBool(_animationName, true);
            //_animator.SetFloat("Direction", -1);
            //DeactivateAnimations();
        }

        private void GetAnimationParameterName()
        {
            for(int i = 0; i < _animator.parameters.Length - 3; i++)
            {
                _playedAnimations = _animator.GetParameter(i);
            }

            _animationName = _playedAnimations.name;
            Debug.Log(_animator.GetParameter(3).name + " current animation name" + _animator.GetBool("Move") + "Played animation name : " + _animationName);
        }

        private void DeactivateAnimations()
        {
            // foreach (var currentAnimation in _playedAnimations)
            // {
            //     if (_animationName == currentAnimation.clip.name)
            //     {
            //         _animator.SetBool(currentAnimation.clip.name, false);
            //     }
            // }
        }
    }
}
