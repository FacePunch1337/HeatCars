using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] TMP_Text _text_name;
    [SerializeField] TMP_Text _text_playercount;


    public RoomInfo RoomInfo { get; private set; }

    
    public void SetInfo(RoomInfo info)
    {
        RoomInfo = info;
        _text_name.text = info.Name;
        _text_playercount.text = $"{info.PlayerCount}/10";
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(_text_name.text);
    }
}
