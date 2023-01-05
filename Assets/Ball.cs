using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class Ball : MonoBehaviourPun
{
    private new  Rigidbody rigidbody;
    
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            
            gameObject.GetComponent<Rigidbody>().AddForce(rigidbody.velocity * 3, ForceMode.Impulse);



        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameZone"))
        {
            Vector3 pos = new Vector3(0, 0, 0);
            gameObject.transform.position = pos;
        }
    }
}
