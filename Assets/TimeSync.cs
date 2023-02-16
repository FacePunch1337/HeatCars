using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TimeSync : MonoBehaviourPun
{
    private string time;
    
    void Start()
    {
        time = GetComponent<Text>().text;
    }

    public void SendTime()
    {
        string[] data = { time };
        photonView.RPC("SyncTimeValue", RpcTarget.AllBuffered, data);
    }

    [PunRPC]
    private void SyncTimeValue(string data)
    {
        time = data;
    }
    void FixedUpdate()
    {
        if (photonView.Owner.IsMasterClient) {
            SendTime();
        }
       
    }
}
