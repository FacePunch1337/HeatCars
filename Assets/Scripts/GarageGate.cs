using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class GarageGate : MonoBehaviourPun
{

    private Vector3 up;
    private Vector3 narrow;
    private bool open = false;
    
    void Update()
    {
        up = transform.position;
        narrow = transform.localScale;

        SendGateStateDate();
    }

    public void SendGateStateDate()
    {
        photonView.RPC("OpenCloseGate", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void OpenCloseGate()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (!open)
            {
                while (up.y < 10f)
                {
                    up.y++;
                    transform.position = up;
                    Debug.Log(up.y);
                    while (narrow.y > 3)
                    {
                        narrow.y--;
                        transform.localScale = narrow;
                        Debug.Log(narrow.y);
                    }
                }

                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                open = true;
            }
            else
            {
                while (up.y > 5.5361)
                {
                    up.y--;
                    transform.position = up;
                    Debug.Log(up.y);
                    while (narrow.y < 10.44725)
                    {
                        narrow.y++;
                        transform.localScale = narrow;
                        Debug.Log(narrow.y);
                    }
                }

                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                open = false;
            }



        }
    }
}
