using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class SelectedButtonScript : MonoBehaviour
{

    
   
    void Start()
    {
        SelectButton();
    }

    public void SelectButton()
    {
        gameObject.GetComponent<Button>().Select();
    }



}
