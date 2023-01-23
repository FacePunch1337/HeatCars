using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

using Smooth;
using System;
using TMPro;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class GameManager : MonoBehaviourPun
{

    private new PhotonView photonView;
    
    public GameObject menuPanel;
    public GameObject adminPanel;
    public GameObject customPanel;
    //public GameObject policeCar;

    public bool first_press = true;
    public bool menu = false;
    public bool adminPanelOpen = false;
   


    void Start()
    {
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
        adminPanel.SetActive(false);
        photonView = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    void Update()
    {
        // Cursor.visible = false;
        // CountOfPlayer();  


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu)
            {
                menuPanel.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                menu = true;
            }
            else
            {
               
                menuPanel.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                menu = false;
            }
           
            
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            
                if (!adminPanelOpen)
                {
                    adminPanel.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    adminPanelOpen = true;
                }
                else
                {

                    adminPanel.SetActive(false);
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    adminPanelOpen = false;
                }
            
            



        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            customPanel.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            customPanel.SetActive(true);
        }


        /*if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(1);
            
        }*/
    }


    public void StartMode()
    {
       
        GameObject.Find("Trigger").TryGetComponent(out Trigger trigger);
        if (trigger.readyCount == PhotonNetwork.PlayerList.Length)
        {
            trigger.start = true;
            
            PhotonNetwork.CurrentRoom.IsOpen = false;
            
        }


    }


    public void SendEndGameMode()
    {
        photonView.RPC("EndGameMode", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void EndGameMode()
    {

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
        PhotonNetwork.LeaveRoom();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeButton()
    {
        menuPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void InputButton()
    {
        menuPanel.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    

}
