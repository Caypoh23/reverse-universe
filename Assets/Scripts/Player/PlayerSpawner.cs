using System.Xml.Linq;
using System.Runtime.InteropServices;
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
        _dataService.SavePlayerPosition(playerPosition.position);
    }

    public void SavePlayerPosition()
    {
        _dataService.SavePlayerPosition(playerPosition.position);
    }
}
