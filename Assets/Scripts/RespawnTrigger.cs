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
                var randomSpawnPos = spawn.spawnPoints[UnityEngine.Random.Range(0, spawn.spawnPoints.Length)].transform;
                float[] posCoords = { randomSpawnPos.position[0], randomSpawnPos.position[1], randomSpawnPos.position[2] };
                //float[] rotCoords = { randomSpawnPos.rotation[0], randomSpawnPos.rotation[1], randomSpawnPos.rotation[2] };

                car.Teleport(posCoords);
            }
            else if (modManager.startMod)
            {
                var randomSpawnPos = modManager.spawnPointsKOB[UnityEngine.Random.Range(0, modManager.spawnPointsKOB.Length)].transform;
                float[] posCoords = { randomSpawnPos.position[0], randomSpawnPos.position[1], randomSpawnPos.position[2] };
               // float[] rotCoords = { randomSpawnPos.rotation[0], randomSpawnPos.rotation[1], randomSpawnPos.rotation[2] };

                car.Teleport(posCoords);
            }
            


        }
         else return;

    }
}
