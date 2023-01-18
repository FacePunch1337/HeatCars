using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameProps : MonoBehaviour
{
    [SerializeField] public GameObject[] _props;
    private string buttonName;
    private int _propID;
    public int propID { get { return _propID; } set { _propID = value; } }



    public void SelectProp()
    {
        buttonName = EventSystem.current.currentSelectedGameObject.name;
        _propID = Convert.ToInt32(buttonName);
        Debug.Log(_propID);
    }
}
