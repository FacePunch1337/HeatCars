using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class SaveButtonColor : MonoBehaviourPun
{

   
    public Color color;




    [PunRPC]
    public void SaveButtonColorPls(Button button)
    {

       // color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;


        Debug.Log(color.ToString());
    }


    
}
