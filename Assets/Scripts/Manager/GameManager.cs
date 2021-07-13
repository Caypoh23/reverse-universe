using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;
    
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private float _respawnTimeStart;

    private bool _canRespawn;

    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        _respawnTimeStart = Time.time;

        _canRespawn = true;
    }

    private void CheckRespawn()
    {
        if (Time.time >= _respawnTimeStart + respawnTime && _canRespawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            virtualCamera.m_Follow = playerTemp.transform;
            _canRespawn = false;
        }
    }
}
