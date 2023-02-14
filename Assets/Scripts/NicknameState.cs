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

        CheckTarget();
        // transform.SetPositionAndRotation(point.localPosition, target.localRotation);
        
        
    }

    public void CheckTarget()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
        else Destroy(target);
    }
}
