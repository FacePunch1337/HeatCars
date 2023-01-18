using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceLight : MonoBehaviour
{
    [SerializeField] private Light[] lights;

    private void Start()
    {
       lights = GetComponents<Light>();
    }
    void FixedUpdate()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            lights[0].enabled = false;
            lights[1].enabled = false;
        }*/
    }
}
