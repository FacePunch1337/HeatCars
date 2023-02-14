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
    [SerializeField] Room _item_prefab;
    [SerializeField] RectTransform _content;
    [SerializeField] GameObject roomListPanel;
    List<Room> _all_rooms_info = new List<Room>();
    private Room list_item;


    void Start()
    {
        menuCanvas.enabled = false;
        roomListPanel.SetActive(false);
        cars.SetActive(false);
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public override void OnConnectedToMaster()
    {
        //loadingCanvas.enabled = false;
        menuCanvas.enabled = true;
        cars.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("name", inputFieldNickname.text);
        PhotonNetwork.NickName = inputFieldNickname.text;
    }

    public void RoomListEnable()
    {
        roomListPanel.SetActive(true);
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
        if (inputFieldNickname.text != string.Empty)
        {
            PhotonNetwork.JoinRoom(inputFieldRoom.text);
        }
        else return;
    }

    

    public override void OnJoinedRoom()
    {
        if (inputFieldNickname.text != string.Empty)
        {
            if (PhotonNetwork.CurrentRoom.IsOpen)
            {
                PhotonNetwork.LoadLevel("TestRoom");
            }
            else return;


        }
        else return;
        



    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _all_rooms_info.FindIndex(x => x.RoomInfo.Name == info.Name);
                Debug.Log(index);
                if (index != -1)
                {

                    Destroy(_all_rooms_info[index].gameObject);
                    _all_rooms_info.RemoveAt(index);



                }
            }
            else
            {
                _item_prefab.TryGetComponent(out Room room);
                list_item = room;

                list_item = Instantiate(_item_prefab, _content);

                if (list_item != null)
                {
                    list_item.SetInfo(info);
                    _all_rooms_info.Add(list_item);
                }
            }

        }
    }

    



  

}
