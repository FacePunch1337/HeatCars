using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static GarageScript;
public class GarageScript : MonoBehaviourPun
{
    public GameObject customPanel; 
    
    private Collider _other; 
    // Start is called before the first frame update
    void Start()
    {
        customPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        _other = other;
        if (other.gameObject.CompareTag("Car"))
        {
            if (other.GetComponent<PhotonView>().Owner.IsLocal)
            {
                customPanel.GetComponentInChildren<Button>();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                customPanel.SetActive(true);
                GameObject.Find("FirstColorButton").GetComponent<Button>().Select();
            }
                

        }
        else return;

        //  textReadyCount.SetActive(false);
        /*if (!other.gameObject.TryGetComponent(out CustomCar customCar)) return;
        else if (base.photonView.IsMine) customCar.SendCustomCarDate();*/


        // Debug.Log("leave trigger");


    }

    private void OnTriggerExit(Collider other)
    {
        _other = other;
        if (other.gameObject.CompareTag("Car"))
        {
            if (other.GetComponent<PhotonView>().Owner.IsLocal)
            {
                customPanel.SetActive(false);

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else return;

        //  textReadyCount.SetActive(false);
        /*if (!other.gameObject.TryGetComponent(out CustomCar customCar)) return;
        else if (base.photonView.IsMine) customCar.SendCustomCarDate();*/


        // Debug.Log("leave trigger");


    }

    public void Customize()
    {
        if (_other.GetComponent<PhotonView>().IsMine)
        {
            _other.gameObject.TryGetComponent(out CustomCar customCar);
            var color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
  
            customCar.Send(color);
        }
            

    }
}




//switch (choice)
//{
//    case "RedButton":
//        color = Color.red;
//        break;
//    case "GreenButton":
//        color = Color.green;
//        break;
//    case "BlueButton":
//        color = Color.blue;
//        break;
//    case "YellowButton":
//        color = Color.yellow;
//        break;
//    case "CyanButton":
//        color = Color.cyan;
//        break;
//    case "MagentaButton":
//        color = Color.magenta;
//        break;
//    case "WhiteButton":
//        color = Color.white;
//        break;
//    case "BlackButton":
//        color = Color.black;
//        break;
//}