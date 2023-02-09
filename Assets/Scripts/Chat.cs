using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class Chat : MonoBehaviour
{

    public TMP_InputField _input_field;
    public GameObject _GO_message;
    public GameObject _content;
    public GameObject scrollView;
    private string _chatMessage;
    private string _lastMassage;
    public string chatMessage { get { return _chatMessage; } set { _chatMessage = value; } }
    private string _chatId;
    public string chatId { get { return _chatId; } set { _chatId = value; } }
    private bool _chatOpen;
    public bool chatOpen { get { return _chatOpen; } set { _chatOpen = value; } }
    private void Start()
    {
        chatOpen = false;
        _input_field.gameObject.SetActive(true);
        
    }



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            if (!chatOpen)
            {

                //_input_field.gameObject.SetActive(true);
               
                _input_field.interactable = true;
                _input_field.text = string.Empty;
                _input_field.Select();

                chatOpen = true;

            }
            else if (chatOpen && _input_field.text != string.Empty)
            {


                SendMessage();
                _input_field.text = string.Empty;
                _input_field.interactable = false;
                
                //_input_field.gameObject.SetActive(false);
                chatOpen = false;

            }
            else if (chatOpen && _input_field.text == string.Empty)
            {
                // _input_field.gameObject.SetActive(false);
                _input_field.interactable = false;
                chatOpen = false;

            }


        }
        else return;

       



    }


    public bool SendMessage()
    {
        string[] receive_messages = { $"{PhotonNetwork.LocalPlayer.ActorNumber}", $"{PhotonNetwork.NickName}: {_input_field.text}", $"{_input_field.text}" };
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.AllBuffered, receive_messages);

        return true;

        



    }

    [PunRPC]
    public void GetMessage(string[] receive_messages)
    {

        
        GameObject M = Instantiate(_GO_message, Vector3.zero, Quaternion.identity, _content.transform);

        M.GetComponent<Message>()._my_message.text = receive_messages[1];

            if (receive_messages[1].Count() > 35)
            {
                M.GetComponent<Message>()._my_message_height.y = 40f;
                
            }
      
            else if (receive_messages[1].Count() > 25 )
            {
                M.GetComponent<Message>()._my_message_height.y = 20f;
                
            }
            else
            {
                M.GetComponent<Message>()._my_message_height.y = 15f;
                
            }

        scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition -= 100;
        if (receive_messages[0] == PhotonNetwork.LocalPlayer.ActorNumber.ToString())
        {

            
            _chatMessage = receive_messages[2];



            Invoke("SendClearMyMessage", 3);
            //if (_chatMessage == last)
            // {

            // }



        }
       


    }

    public void SendClearMyMessage()
    {
        
        string _params = string.Empty;
        GetComponent<PhotonView>().RPC("ClearMyMessage", RpcTarget.All, _params);
    }
    [PunRPC]
    public void ClearMyMessage(string _params)
    {
        
        _chatMessage = _params;


    }

   

}
