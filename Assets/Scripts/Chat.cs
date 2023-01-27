using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{

    public TMP_InputField _input_field;
    public GameObject _GO_message;
    public GameObject _content;
    private bool chatOpen;
    private void Start()
    {
        chatOpen = false;
        _input_field.gameObject.SetActive(false);

    }

    

    private void FixedUpdate()
    {
        if (chatOpen)
        {
            _input_field.Select();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            

            if (!chatOpen)
            {
                chatOpen = true;
                _input_field.gameObject.SetActive(true);
               
            }
            else if (chatOpen && _input_field.text == string.Empty)
            {
                _input_field.gameObject.SetActive(false);
                chatOpen = false;
                
            }
            else if (chatOpen && _input_field.text != string.Empty)
            {
                SendMessage();
                _input_field.gameObject.SetActive(false);
                chatOpen = false;
                
            }
            
            
            
        }
        


        
    }
    public void SendMessage()
    {
        
            GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.AllBuffered, $"{PhotonNetwork.NickName}: {_input_field.text}");
            _input_field.text = "";

    }

    [PunRPC]
    public void GetMessage(string receive_message)
    {
        GameObject M = Instantiate(_GO_message, Vector3.zero, Quaternion.identity, _content.transform);

        M.GetComponent<Message>()._my_message.text = receive_message;
    }
}
