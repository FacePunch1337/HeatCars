using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class NicknameState : MonoBehaviour
{

    private Transform target;
  //  public Transform point;
   

    private void Start()
    {

        
        
        target = GameObject.Find("CM FreeLook1").GetComponent<FollowCamera>().transform;       
    }

    void Update()
    {
      // transform.SetPositionAndRotation(point.localPosition, target.localRotation);
        transform.LookAt(target.transform);
    }
}
