using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public TMP_Text _my_message;
    public Vector2 _my_message_height;
  
    
    void Start()
    {
        
        gameObject.GetComponent<RectTransform>().sizeDelta = _my_message_height;
        

        GetComponent<RectTransform>().SetAsFirstSibling();
    }
    
}
