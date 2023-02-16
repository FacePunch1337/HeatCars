using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Photon.Pun;

using Photon.Realtime;
using System.Reflection;
using ExitGames.Client.Photon;
using System;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;


public class CustomCar : MonoBehaviourPun
{
    private int sceneNum;
    //RiseEvent

    /*private const byte COLOR_CHANGE_EVENT = 0;
   
    void Start()
    {
      
    }

    private void Update()
    {
        if (photonView.Owner.IsMasterClient && Input.GetKey(KeyCode.Q))
        {
            
            ChangeColor();
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventRecived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventRecived;
    }

    private void NetworkingClient_EventRecived(EventData obj)
    {
        if(obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            float r = (float)datas[0];
            float g = (float)datas[1];
            float b = (float)datas[2];

            foreach (Material item in GameObject.Find("Car(Clone)").GetComponent<MeshRenderer>().materials)
            {
                item.color = new Color(r, g, b, 1f);
            }
        }
    }

    private void ChangeColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        foreach (Material item in GameObject.Find("Car(Clone)").GetComponent<MeshRenderer>().materials)
        {
            item.color = new Color(r, g, b, 1f);
        }

        object[] datas = new object[] {r, g, b };

        PhotonNetwork.RaiseEvent(COLOR_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendUnreliable);
       
    }*/

    /*public void Green()
    {
        photonView.RPC("ChangeColorToRed", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ChangeColorToGreen()
    {
        var carMaterialList = gameObject.GetComponent<MeshRenderer>();
        carMaterialList.materials[0].color = Color.green;

    }
    public void Blue()
    {
        photonView.RPC("ChangeColorToRed", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ChangeColorToBlue()
    {
        var carMaterialList = gameObject.GetComponent<MeshRenderer>();
        carMaterialList.materials[0].color = Color.green;

    }*/


    private Color _color;

    private int current_color;
    public Color color { get { return _color; } set { _color = value; } }
    private bool neon;
    private void Start()
    {
        
            //PhotonNetwork.ConnectUsingSettings();
            //PlayerPrefs.SetInt("SelectedColor", current_color);
        
        
    }

    public void SendColor(Color color, bool neon)
    {
        float[] arrColors = { color.r, color.g, color.b, color.a };

        bool[] arrBool = { neon };
            
        photonView.RPC("ChangeColor", RpcTarget.AllBuffered, arrColors, arrBool);
 
    }
    

    [PunRPC]
    public void ChangeColor(float[] arr, bool[] neon)
    {
        Color color = new Color(arr[0], arr[1], arr[2], arr[3]);
        var carMaterialList = gameObject.GetComponent<MeshRenderer>();
        if (neon[0])
        {
            if (gameObject.name.Contains("PoliceCar"))
            {
                carMaterialList.materials[10].color = color;
                //PlayerPrefs.SetString("CarColor", );
            }
            else if (gameObject.name.Contains("Car"))
            {
                carMaterialList.materials[1].color = color;
                // PlayerPrefs.SetInt("CarColor", current_car);
            }
            else if (gameObject.name.Contains("Bus"))
            {
                carMaterialList.materials[3].color = color;
                carMaterialList.materials[6].color = color;
                carMaterialList.materials[8].color = color;
                // PlayerPrefs.SetInt("CarColor", current_car);
            }
        }
        else
        {
            if (gameObject.name.Contains("PoliceCar"))
            {
                carMaterialList.materials[7].color = color;
                //PlayerPrefs.SetString("CarColor", );
            }
            else if (gameObject.name.Contains("NoPoliceCar"))
            {
                var carMaterialListLocal = gameObject.GetComponentInChildren<MeshRenderer>();
                carMaterialListLocal.materials[7].color = color;
                carMaterialListLocal.materials[3].color = color;
                //PlayerPrefs.SetString("CarColor", );
            }
            else if (gameObject.name.Contains("Car"))
            {
                carMaterialList.materials[0].color = color;
                // PlayerPrefs.SetInt("CarColor", current_car);
            }
            else if (gameObject.name.Contains("Bus"))
            {
                carMaterialList.materials[4].color = color;
                carMaterialList.materials[9].color = color;
                // PlayerPrefs.SetInt("CarColor", current_car);
            }
        }


       
       
        PlayerPrefs.SetInt("SelectedColor", current_color);


    }

    

}
