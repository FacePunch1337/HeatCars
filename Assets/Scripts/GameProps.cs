using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameProps : MonoBehaviour
{
    [SerializeField] public GameObject[] _prop;
    private string buttonName;

    private void Start()
    {
        _prop = GetComponents<GameObject>();
        buttonName = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<Text>().text;
    }
    public void SelectProp()
    {
        Debug.Log(buttonName);
    }

}
