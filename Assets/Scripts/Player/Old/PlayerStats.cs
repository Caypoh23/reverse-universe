using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject deathChunkParticle;
    [SerializeField] private GameObject deathBloodParticle;
    [SerializeField] private GameManager gameManager;

    private float _currentHealth;
    
    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        gameManager.Respawn();
        Destroy(gameObject);
    }
}
