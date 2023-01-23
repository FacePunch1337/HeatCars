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
    private Collider otherColider;
    //public GameObject buttonReady;
    public GameObject buttonStart;
    public GameObject modPanel;
    public GameObject textReadyCount;
   
    public bool start;
    public int readyCount = 0;
    
    public PhotonView otherPhotonView { get { return _otherPhotonView; } set { _otherPhotonView = value; } }
    
    private void Start()
    {

        start = false;
        buttonStart.SetActive(false);
        modPanel.SetActive(false);

        //buttonReady.SetActive(false);


    }
    private void Update()
    {
        if (start)
        {
            
            start = false;
        }
        else return;
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
                modPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
                if (readyCount == PhotonNetwork.PlayerList.Length)
                {
                    buttonStart.SetActive(true);
                }
                else
                {
                    buttonStart.SetActive(false);
                }
                
            }
        }
        else return;

       
    }

    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            readyCount--;
            
            modPanel.SetActive(false);
            //buttonStart.SetActive(false);
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