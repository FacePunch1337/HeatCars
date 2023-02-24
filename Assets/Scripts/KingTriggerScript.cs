using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KingTriggerScript : MonoBehaviourPun
{
    public TMP_Text kingName;
    private int playerCount = 0;
    private TMP_Text _lastKingName;
    public TMP_Text lastKingName { get { return _lastKingName; } set { _lastKingName = value; } }
    private Collider _other;

    private void Start()
    {
        _other = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        
         if (other.gameObject.CompareTag("Car"))
         {

            playerCount++;

         }
         else return;

    }
    private void OnTriggerStay(Collider other)
    {
        _other = other;

        ShowKingName();

    }

  
    public void ShowKingName()
    {
        if (_other.gameObject.CompareTag("Car"))
        {
            _other.gameObject.TryGetComponent(out CarController car);
            if (playerCount > 1) kingName.text = "?";
            else kingName.text = car.nickname.text;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {

            playerCount--;

        }
        else return;

        //  textReadyCount.SetActive(false);
        /*if (!other.gameObject.TryGetComponent(out CustomCar customCar)) return;
        else if (base.photonView.IsMine) customCar.SendCustomCarDate();*/


        // Debug.Log("leave trigger");


    }

    /*public void SendKingNameViewDate()
    {
        photonView.RPC("KingNameView", RpcTarget.AllBuffered, "other");

    
    }

    [PunRPC]
    /*public void KingNameView(Collider other)
    {

        if (other.gameObject.CompareTag("Car"))
        {

            other.gameObject.TryGetComponent(out CarController car);
            if (playerCount > 1) kingName.text = "?";
            else kingName.text = car.nickname.text;
        }
        else return;
    }*/


}
