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
    public AudioSource[] audioSources;
    private new Rigidbody rigidbody;

    private float pitchFromCar;
    private float currentSpeed;

    private float maxAngle;

    void Start()
    {

        // driveSource = GetComponent<AudioSource>();

        audioSources = GetComponents<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if(photonView.Owner.IsLocal) EngineSound();

        if (Input.GetKeyDown(KeyCode.E) && photonView.IsMine)
        {
            SendBeepSoundData();
        }

        if (Input.GetKeyDown(KeyCode.Space) && photonView.IsMine)
        {
            SkirtSound();
        }
        if (Input.GetKeyUp(KeyCode.Space) && photonView.IsMine)
        {
            audioSources[2].Stop();
        }

    }

    
    public void EngineSound()
    {
        currentSpeed = rigidbody.velocity.magnitude;
        pitchFromCar = rigidbody.velocity.magnitude / 100f;

        if (currentSpeed < minSpeed)
        {
            audioSources[0].pitch = minPitch;
        }

        if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            audioSources[0].pitch = minPitch + pitchFromCar;
        }

        if (currentSpeed > maxSpeed)
        {
            audioSources[0].pitch = maxPitch;
        }
       

    }

    

    public void SendBeepSoundData()
    {
        photonView.RPC("Beep", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Beep()
    {
        audioSources[1].Play();
    }

    public void SkirtSound()
    {
        if (currentSpeed > 10)
        {
            audioSources[2].Play();
        }
        
    }


}
