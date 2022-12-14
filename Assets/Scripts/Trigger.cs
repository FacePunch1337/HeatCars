using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using System;
using UnityEngine.UI;

public class Trigger : MonoBehaviourPun
{



    private CustomCar customCar;
    private PhotonView _otherPhotonView;
    //public GameObject buttonReady;
    public GameObject buttonStart;
    public GameObject textReadyCount;
    
    private bool start;
    public int readyCount = 0;

    public PhotonView otherPhotonView { get { return _otherPhotonView; } set { _otherPhotonView = value; } }

    private void Start()
    {
        
       
        buttonStart.SetActive(false);
        //buttonReady.SetActive(false);
       

    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Car")) 
        {
            readyCount++;
        } 
        else return;

    }
    private void OnTriggerStay(Collider other)
    {

      
        if (other.gameObject.CompareTag("Car"))
        {
            if (other.GetComponent<PhotonView>().Owner.IsMasterClient && other.GetComponent<PhotonView>().AmOwner)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                buttonStart.SetActive(true);
            }
        }
        else return;

       
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            readyCount--;
            buttonStart.SetActive(false);
            //buttonReady.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else return;

        //  textReadyCount.SetActive(false);
        /*if (!other.gameObject.TryGetComponent(out CustomCar customCar)) return;
        else if (base.photonView.IsMine) customCar.SendCustomCarDate();*/


        // Debug.Log("leave trigger");


    }
}