using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public TMP_Text _my_message;
    // Start is called before the first frame update
    
    void Start() => GetComponent<RectTransform>().SetAsFirstSibling();
}
