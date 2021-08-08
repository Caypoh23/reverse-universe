using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get
        {
            if (_movement != null)
            {
                return _movement;
            }

            Debug.LogError("No Movement Core Component on " + transform.parent.name);
            return null;
        }
        private set => _movement = value;
    }

    public CollisionSenses CollisionSenses
    {
        get
        {
            if (_collisionSenses != null)
            {
                return _collisionSenses;
            }

            Debug.LogError("No CollisionSenses Core Component on " + transform.parent.name);
            return null;
        }
        private set => _collisionSenses = value;
    }

    private Movement _movement;
    private CollisionSenses _collisionSenses;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
    }
}