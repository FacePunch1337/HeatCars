using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CarSounds : MonoBehaviourPun
{


    public float minPitch;
    public float maxPitch;
    public float minSpeed;
    public float maxSpeed;


    //public AudioSource driveSource;
    //public AudioSource skirtSource;
    public AudioSource beepSource;
    private new Rigidbody rigidbody;

    private float pitchFromCar;
    private float currentSpeed;

    private float maxAngle;

    void Start()
    {

        // driveSource = GetComponent<AudioSource>();

        beepSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //EngineSound();
        if (Input.GetKey(KeyCode.E) && photonView.IsMine)
        {
            SendBeepSoundData();
        }

    }
    /* void EngineSound()
    {
        currentSpeed = rigidbody.velocity.magnitude;
        pitchFromCar = rigidbody.velocity.magnitude / 100f;

        if (currentSpeed < minSpeed)
        {
            driveSource.pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            driveSource.pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            driveSource.pitch = maxPitch;
        }
       

    }*/

    /* void SkirtSound()
     {
         if (Input.GetKey(KeyCode.Space))
         {
             skirtSource.Play();
         }




     }*/

    public void SendBeepSoundData()
    {
        photonView.RPC("Beep", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Beep()
    {
        beepSource.Play();
    }

   
}
