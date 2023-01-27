using Photon.Pun.Demo.PunBasics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private SpawnPlayers spawn;
    private ModManager modManager;
    private GameManager gameManager;

    private void Start()
    {
        GameObject.Find("GameManager").TryGetComponent(out GameManager _gameManager);
        gameManager = _gameManager;
        gameManager.TryGetComponent(out SpawnPlayers _spawn);
        gameManager.TryGetComponent(out ModManager _modManager);
        spawn = _spawn;
        modManager = _modManager;
       
    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Car"))
         {
             other.gameObject.TryGetComponent(out CarController car);
            if (!modManager.startMod)
            {
                var randomSpawnPos = spawn.spawnPoints[UnityEngine.Random.Range(0, spawn.spawnPoints.Length)].transform.position;
                float[] coords = { randomSpawnPos[0], randomSpawnPos[1], randomSpawnPos[2] };

                car.Teleport(coords);
            }
            else if (modManager.startMod)
            {
                var randomSpawnPos = modManager.spawnPoints[UnityEngine.Random.Range(0, modManager.spawnPoints.Length)].transform.position;
                float[] coords = { randomSpawnPos[0], randomSpawnPos[1], randomSpawnPos[2] };

                car.Teleport(coords);
            }
            


        }
         else return;

    }
}
