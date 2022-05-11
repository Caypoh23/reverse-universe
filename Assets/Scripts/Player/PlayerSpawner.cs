using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;

    private DataService _dataService;

    private void Start()
    {
        _dataService = DataService.Instance;
        playerPosition.position = _dataService.PlayerPosition;
    }
}
