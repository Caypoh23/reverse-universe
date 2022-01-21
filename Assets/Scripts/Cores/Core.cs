using System;
using System.Collections;
using System.Collections.Generic;
using Cores.CoreComponents;
using Generics;
using Interfaces;
using UnityEngine;

public class Core : MonoBehaviour
{
    
    [SerializeField] private Movement _movement;
    [SerializeField] private CollisionSenses _collisionSenses;
    [SerializeField] private Combat _combat;
    [SerializeField] private Stats _stats;

    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
        private set => _movement = value;
    }

    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
        private set => _collisionSenses = value;
    }

    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(_combat, transform.parent.name);
        private set => _combat = value;
    }

    public Stats Stats
    {
        get => GenericNotImplementedError<Stats>.TryGet(_stats, transform.parent.name);
        private set => _stats = value;
    }

    private readonly List<ILogicUpdate> _components = new List<ILogicUpdate>();

    public void LogicUpdate()
    {
        foreach (var component in _components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(ILogicUpdate component)
    {
        //Check to make sure components is not already part of the list - .Contains() comes from the Linq library we added.
        if (!_components.Contains(component))
        {
            _components.Add(component);
        }
    }
}