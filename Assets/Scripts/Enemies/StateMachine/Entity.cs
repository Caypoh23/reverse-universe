using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine StateMachine;

    public int FacingDirection { get; private set; }

    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject AliveGO { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;

    private Vector2 _velocityWorkspace;

    public virtual void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        Rb = AliveGO.GetComponent<Rigidbody2D>();
        Anim = AliveGO.GetComponent<Animator>();

        StateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkspace.Set(FacingDirection * velocity, Rb.velocity.y);
        Rb.velocity = _velocityWorkspace;
    }

    public virtual void CheckWall()
    {
        
    }
    
    public virtual void CheckLedge(){}
    
}