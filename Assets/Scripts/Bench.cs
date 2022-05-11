using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cores.CoreComponents;

public class Bench : MonoBehaviour
{
    [SerializeField] private Stats playerStats;
    [SerializeField] private Tag playerTag;
    [SerializeField] private Transform playerPosition;

    private DataService _dataService;

    private void Awake()
    {
        _dataService = DataService.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.HasTag(playerTag))
        {
            _dataService.SavePlayerPosition(playerPosition.position);
            playerStats.RestoreFullHealth();
        }
    }
}
