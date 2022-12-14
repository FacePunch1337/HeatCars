using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspDance : MonoBehaviour
{
    
    private float suspDist = 0.1f;
    private float maxSuspDist = 0.8f;
    private float minSuspDist = 0.2f;
  

    private void Start()
    {
     
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            gameObject.GetComponent<WheelCollider>().suspensionDistance += suspDist ;
        }
        if (Input.GetKey(KeyCode.E))
        {
            gameObject.GetComponent<WheelCollider>().suspensionDistance -= suspDist;
        }
    }
}
