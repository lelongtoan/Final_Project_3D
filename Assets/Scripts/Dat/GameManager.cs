﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using JetBrains.Annotations;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject virtualCameraPrefab;
    public GameObject enemyPrefab;
    public CinemachineVirtualCamera virtualCamera;
    public Transform spwanPointPlayer;
    void Start()
    {
        GameObject playerInstance = Instantiate(playerPrefab,new Vector3(spwanPointPlayer.position.x,2,spwanPointPlayer.position.z), Quaternion.identity);

        GameObject cameraInstance = Instantiate(virtualCameraPrefab, new Vector3(0, 1, 0), Quaternion.identity);
        //GameObject enemy=Instantiate(enemyPrefab, new Vector3(7,3, 0), Quaternion.identity);
        if (virtualCamera == null)
        {
            virtualCamera=FindObjectOfType<CinemachineVirtualCamera>();
        }
        if (playerInstance != null && virtualCamera != null)
        {
            virtualCamera.Follow = playerInstance.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
