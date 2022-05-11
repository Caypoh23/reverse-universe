using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cores.CoreComponents;

public class Acid : MonoBehaviour
{
    [SerializeField] private Stats playerStats;
    [SerializeField] private Tag playerTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.HasTag(playerTag))
        {
            playerStats.TakeDamage(1000);
        }
    }
}
