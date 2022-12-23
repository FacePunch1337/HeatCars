using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UIElements;

public class GarageGate : MonoBehaviourPun
{
    private AudioSource[] gateSources;
    private Vector3 up;
    private Vector3 narrow;
    private float _pos;
    private float _size;
    private bool opening = false;
    private bool isOpen = false;
    private bool inAction = false;

    private void Start()
    {
        gateSources = GetComponents<AudioSource>();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        _pos = gameObject.transform.position.y;
        _size = gameObject.transform.localScale.y;
    }
    void Update()
    {

        if (!opening)
        {
            CloseGate();
        }
        else return;
        



    }

  
    /*public void SendGateStateDate()
    {
        photonView.RPC("OpenCloseGate", RpcTarget.AllBuffered);
    }*/

    //[PunRPC]

    public void GateSoundPlay()
    {
        gateSources[0].Play();
        
    }

    public void GateSoundStop()
    {
        gateSources[0].Stop();
        
    }

    
   
    public void OpenGate()
    {


        if (gameObject.transform.position.y > 9f) 
        {
            isOpen = true;
            GateSoundStop();
            

        } 
        else
        {
           
            up = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1 * Time.deltaTime, gameObject.transform.position.z);
            gameObject.transform.position = up;
           

            narrow = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y - 6 * Time.deltaTime, gameObject.transform.localScale.z);
            gameObject.transform.localScale = narrow;


            opening = true;

            
           
        }
           
        
        

    }

    public void CloseGate()
    {
        

        if (gameObject.transform.localScale.y > 30f)
        {
            isOpen = false;
            
            GateSoundStop();
            
        }
        else
        {
            up = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1 * Time.deltaTime, gameObject.transform.position.z);
            gameObject.transform.position = up;

            narrow = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y + 6 * Time.deltaTime, gameObject.transform.localScale.z);
            gameObject.transform.localScale = narrow;


            opening = false;

            


        }
        
        


    }

}
