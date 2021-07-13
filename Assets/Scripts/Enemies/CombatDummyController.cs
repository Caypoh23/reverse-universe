using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CombatDummyController : MonoBehaviour
{
    [SerializeField] private Animator aliveAnim;

    [SerializeField] private float maxHealth;
    [SerializeField] private float knockBackSpeedX;
    [SerializeField] private float knockBackSpeedY;
    [SerializeField] private float knockBackDuration;
    [SerializeField] private float knockBackDeathSpeedX;
    [SerializeField] private float knockBackDeathSpeedY;
    [SerializeField] private float deathTorque;
    [SerializeField] private bool applyKnockBack;

    private float _currentHealth;
    private float _knockBackStart;
    private int _playerFacingDirection;
    private bool _playerOnLeft;
    private bool _knockBack;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private Tag hitParticleTag;

    [SerializeField] private GameObject aliveGO;
    [SerializeField] private GameObject brokenTopGO;
    [SerializeField] private GameObject brokenBottomGO;

    [SerializeField] private Rigidbody2D rbAlive;
    [SerializeField] private Rigidbody2D rbBrokenTop;
    [SerializeField] private Rigidbody2D rbBrokenBottom;
    private static readonly int PlayerOnLeft = Animator.StringToHash("playerOnLeft");
    private static readonly int DamageAnim = Animator.StringToHash("damage");

    private void Awake()
    {
        _currentHealth = maxHealth;
        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenBottomGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockBack();
    }

    private void Damage(float[] details)
    {
        _currentHealth -= details[0];

        if (details[1] < aliveGO.transform.position.x)
        {
            _playerFacingDirection = 1;
        }
        else
        {
            _playerFacingDirection = -1;
        }

        ObjectPooler.Instance.SpawnFromPool(hitParticleTag, aliveGO.transform.position,
            Quaternion.Euler(0.0f, 0.0f, UnityEngine.Random.Range(0.0f, 360.0f)));

        _playerOnLeft = _playerFacingDirection == 1;

        aliveAnim.SetBool(PlayerOnLeft, _playerOnLeft);
        aliveAnim.SetTrigger(DamageAnim);

        if (applyKnockBack && _currentHealth > 0.0f)
        {
            KnockBack();
        }

        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void KnockBack()
    {
        _knockBack = true;
        _knockBackStart = Time.time;
        rbAlive.velocity = new Vector2(knockBackSpeedX * _playerFacingDirection, knockBackSpeedY);
    }

    private void CheckKnockBack()
    {
        if (Time.time >= _knockBackStart + knockBackDuration && _knockBack)
        {
            _knockBack = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBottomGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBottomGO.transform.position = aliveGO.transform.position;

        rbBrokenBottom.velocity = new Vector2(knockBackSpeedX * _playerFacingDirection, knockBackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockBackDeathSpeedX * _playerFacingDirection, knockBackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque * -_playerFacingDirection, ForceMode2D.Impulse);
        
    }
}