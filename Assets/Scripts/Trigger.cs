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
   
    public GameObject textReadyCount;
    public GameObject modManagerObj;
    private ModManager modManager;
   
    public bool _modStartFlag;
    public int readyCount = 0;
    
    public PhotonView otherPhotonView { get { return _otherPhotonView; } set { _otherPhotonView = value; } }
    
    private void Start()
    {

        modManagerObj.TryGetComponent(out ModManager _modManager);
        modManager = _modManager;
        
       

        //buttonReady.SetActive(false);


    }
    private void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Car")) 
        {
            modManager.mod = ModManager.Mod.KOB;
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
                modManager.modPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                
                if (readyCount == PhotonNetwork.PlayerList.Length)
                {
                    modManager.buttonStart.SetActive(true);
                }
                else
                {
                    modManager.buttonStart.SetActive(false);
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

            modManager.modPanel.SetActive(false);
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