using Interfaces;
using UnityEngine;

public class TakeDamageCommand : ICommand
{
    private float _currentHealthAmount;

    private float _damageAmount;

    public TakeDamageCommand(float currentHealthAmount, float damageAmount)
    {
        _currentHealthAmount = currentHealthAmount;
        _damageAmount = damageAmount;
    }

    public void Execute()
    {
        _currentHealthAmount -= _damageAmount;

        Debug.Log("removed and became " + _currentHealthAmount);
    }

    public void Undo()
    {
        _currentHealthAmount += _damageAmount;
        Debug.Log("revised and became " + _currentHealthAmount);
    }
}
