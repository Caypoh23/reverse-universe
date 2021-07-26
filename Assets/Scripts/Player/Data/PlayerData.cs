﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/ Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")] 
    public float movementVelocity = 10f;

    [Header("Wall Jump State")] 
    public float wallJumpVelocity = 20f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);
    
    [Header("Jump State")] 
    public float jumpVelocity = 15;
    public int amountOfJumps = 1;

    [Header("Wall Slide State")] 
    public float wallSlideVelocity = 3f;

    [Header("In Air State")] 
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.5f;

    [Header("Check Variables")] 
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
}