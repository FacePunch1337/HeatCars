using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class GarageGate : MonoBehaviourPun
{

    private Vector3 up;
    private Vector3 narrow;
    private float _pos;
    private float _size;
    private bool open = false;

    private void Start()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        _pos = gameObject.transform.position.y;
        _size = gameObject.transform.localScale.y;
    }
    void Update()
    {
        
        if (!open)
        {
            CloseGate();
        }
        



    }

  
    /*public void SendGateStateDate()
    {
        photonView.RPC("OpenCloseGate", RpcTarget.AllBuffered);
    }*/

    //[PunRPC]
    public void OpenGate()
    {
        if (gameObject.transform.position.y > 9f) return;
        else
        {
            
            up = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1 * Time.deltaTime, gameObject.transform.position.z);
            gameObject.transform.position = up;
           

            narrow = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - 6 * Time.deltaTime, gameObject.transform.localScale.z);
            gameObject.transform.localScale = narrow;

            
            open = true;
        }
           
        
        

    }

    public void CloseGate()
    {
        if (gameObject.transform.localScale.y > 30f) return;
        else
        {
            up = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1 * Time.deltaTime, gameObject.transform.position.z);
            gameObject.transform.position = up;

            narrow = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y + 6 * Time.deltaTime, gameObject.transform.localScale.z);
            gameObject.transform.localScale = narrow;

            
            open = false;
        }
        
        


    }

}
