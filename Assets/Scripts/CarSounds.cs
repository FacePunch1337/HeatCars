using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Animations;

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
    private bool engineStart = false;

    void Start()
    {
       
        // driveSource = GetComponent<AudioSource>();

        audioSources = GetComponents<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (photonView.Owner.IsLocal)
        {

        
            if (Input.GetKeyDown(KeyCode.K))
            {
                SendEngineStartSoundData();
            }


            //EngineSound();

            SendEngineSoundData();



            if (Input.GetKeyDown(KeyCode.E))
            {
                SendBeepSoundData();

                //Beep();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendSkirtSoundData();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                audioSources[2].Stop();
            }

    }
        
       
        

    }

 

    public void SendEngineSoundData()
    {
       
            photonView.RPC("EngineSound", RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    public void EngineSound()
    {
        
            if (engineStart)
            {
                audioSources[0].Play();
                engineStart = false;
            }



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

    public void SendEngineStartSoundData()
    {
        
            photonView.RPC("EngineStartSound", RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    public void EngineStartSound()
    {
        
            audioSources[3].Play();
            engineStart = true;
        
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


    public void SendSkirtSoundData()
    {
       
            photonView.RPC("SkirtSound", RpcTarget.AllBuffered);
        
    }

    [PunRPC]
    public void SkirtSound()
    {
            if (currentSpeed > 10)
            {
                audioSources[2].Play();
            }
        
        /*if(currentSpeed > 30 && Input.GetKeyDown(KeyCode.A))
        {
            audioSources[2].Play();
        }
        else audioSources[2].Stop();

        if (currentSpeed > 30 && Input.GetKeyDown(KeyCode.D))
        {
            audioSources[2].Play();
        }
        else audioSources[2].Stop();*/

    }

    
    private void OnCollisionEnter(Collision collision)
    {
            if(collision.gameObject.CompareTag("MetalProp"))
            {
                
                audioSources[4].volume = collision.impulse.magnitude * 0.01f;
                
                audioSources[4].Play();
            }
            
    }

}
