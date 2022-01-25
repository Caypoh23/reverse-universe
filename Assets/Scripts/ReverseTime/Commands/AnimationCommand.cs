using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class AnimationCommand : ICommand
{
    #region Animation

    private Animator _animator;
    private string _previousAnimationName;
    private bool _previousAnimationStatus;

    #endregion

    public AnimationCommand(Animator animator)
    {
        _animator = animator;
    }

    public void Execute()
    {
        GetAnimationParameterName();
    }

    public void Undo()
    {
        _animator.SetBool(_previousAnimationName, true);
        DeactivateAnimations();
    }

    private void GetAnimationParameterName()
    {
        foreach (var animatorParameter in _animator.parameters)
        {
            if (IsAnimationParameterBool(animatorParameter) && _animator.GetBool(animatorParameter.name))
            {
                _previousAnimationName = animatorParameter.name;
                _previousAnimationStatus = _animator.GetBool(animatorParameter.name);
                break;
            }
        }
    }

    private void DeactivateAnimations()
    {
        foreach (var animatorParameter in _animator.parameters)
        {
            if (_previousAnimationName != animatorParameter.name && IsAnimationParameterBool(animatorParameter))
            {
                _animator.SetBool(animatorParameter.name, false);
            }
        }
    }

    private bool IsAnimationParameterBool(AnimatorControllerParameter animatorControllerParameter) => animatorControllerParameter.type == AnimatorControllerParameterType.Bool;
}
