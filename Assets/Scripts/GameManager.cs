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

public class GameManager : MonoBehaviourPunCallbacks
{


    private PhotonView photonView;
    public bool menu = false;
    public bool adminPanelOpen = false;


    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject adminPanel;
    private Chat _chat;
    public Chat chat { get { return _chat; } set { _chat = value; } }
    private ModManagerModule _modManager;
    public ModManagerModule modManager { get { return _modManager; } set { _modManager = value; } }


    void Start()
    {
        _modManager = gameObject.GetComponent<ModManagerModule>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuPanel.SetActive(false);
        adminPanel.SetActive(false);

        photonView = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        GameObject.Find("ChatPanel").TryGetComponent(out Chat chat_);
        _chat = chat_;
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

            if (!adminPanelOpen && !chat.chatOpen)
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
        else return;

       

        if (Input.GetKeyDown(KeyCode.P))
        {
            LeaveGameRoom();


        }
    }


    public void LeaveGameRoom()
    {

        PhotonNetwork.LeaveRoom();

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
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
