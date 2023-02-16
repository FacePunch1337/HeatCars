using Photon.Realtime;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] TMP_Text _text_name;
    [SerializeField] TMP_Text _text_playercount;
    private string nickNameInputFieldText;

    private void Start()
    {
        nickNameInputFieldText = GameObject.Find("NickNameInputFieldText").GetComponent<Text>().text;
    }
    public RoomInfo RoomInfo { get; private set; }

    
    public void SetInfo(RoomInfo info)
    {
        RoomInfo = info;
        _text_name.text = info.Name;
        _text_playercount.text = $"{info.PlayerCount}/10";
    }

    public void JoinRoom()
    {
        if (nickNameInputFieldText != string.Empty)
        {
            PhotonNetwork.JoinRoom(_text_name.text);
        }
        else return;
    }
}
