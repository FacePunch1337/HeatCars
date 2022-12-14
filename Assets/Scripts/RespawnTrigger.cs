using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Car"))
         {
             other.gameObject.TryGetComponent(out CarController car);
             car.Respawn();


         }
         else return;

    }
}
