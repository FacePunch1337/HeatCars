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
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviourPun
{


    private PhotonView photonView;
    public bool menu = false;
    public bool adminPanelOpen = false;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject adminPanel;
    private ModManager _modManager;
    public ModManager modManager { get { return _modManager; } set { _modManager = value; } }


    void Start()
    {
        _modManager = gameObject.GetComponent<ModManager>();
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

       

        /*if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene(1);
            
        }*/
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
