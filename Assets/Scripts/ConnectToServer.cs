using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun.Demo.Cockpit;
//using UnityEditor.PackageManager.UI;

public class ConnectToServer : MonoBehaviourPunCallbacks
{

    public Canvas loadingCanvas;
    public Canvas menuCanvas;
    public GameObject cars;
    public InputField inputFieldRoom;
    public InputField inputFieldNickname;

    public bool closeRoom;


    void Start()
    {
        menuCanvas.enabled = false;
        cars.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loadingCanvas.enabled = false;
        menuCanvas.enabled = true;
        cars.SetActive(true);
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", inputFieldNickname.text);
        PhotonNetwork.NickName = inputFieldNickname.text;
    }
    public void CreateRoom()
    {
        if (inputFieldNickname.text != string.Empty && inputFieldRoom.text != string.Empty)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 5;
            PhotonNetwork.CreateRoom(inputFieldRoom.text);
        }
        else return;
    }

    public void JoinRoom()
    {
        if (inputFieldNickname.text != string.Empty && inputFieldRoom.text != string.Empty)
        {
            PhotonNetwork.JoinRoom(inputFieldRoom.text);
        }
        else return;
    }

    public override void OnJoinedRoom()
    {
        if (inputFieldNickname.text != string.Empty && inputFieldRoom.text != string.Empty)
        {
            if (PhotonNetwork.CurrentRoom.IsOpen)
            {
                PhotonNetwork.LoadLevel("TestRoom");
            }
            else return;


        }
        else return;
        



    }

    public void Exit()
    {
        /*menuCanvas.enabled = true;
        PhotonNetwork.LeaveRoom();*/
        Application.Quit();
    }

}
