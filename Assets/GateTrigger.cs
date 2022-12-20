using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            GameObject.Find("GarageGate1").TryGetComponent(out GarageGate garageGate);
            garageGate.OpenGate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            GameObject.Find("GarageGate1").TryGetComponent(out GarageGate garageGate);
            garageGate.CloseGate();
        }
    }

}
