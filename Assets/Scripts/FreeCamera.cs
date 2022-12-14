using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public GameObject tPlayer;
    public Transform tFollowTarget;
    public PhotonView view;
    private CinemachineVirtualCamera vcam;



    //private Transform player;
    public void Attach(Transform _transform)
    {
        vcam = GetComponent<CinemachineVirtualCamera>();

        vcam.transform.parent = _transform;
        vcam.transform.position = Vector3.zero;
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        float aimX = Input.GetAxis("Mouse X");
        float aimY = Input.GetAxis("Mouse Y");
        tFollowTarget.rotation *= Quaternion.AngleAxis(aimX * 10, Vector3.up);
        tFollowTarget.rotation *= Quaternion.AngleAxis(-aimY * 10, Vector3.right);


        var angleX = tFollowTarget.localEulerAngles.x;
        Debug.Log(angleX);
        if (angleX > 180 && angleX < 10)
        {
            angleX = 100;
        }
        else if (angleX < 180 && angleX > 10)
        {
            angleX = 10;
        }

        tFollowTarget.localEulerAngles = new Vector3(angleX, tFollowTarget.localEulerAngles.y, 0);

        /*if (stickCamera)
        {
            playerModel.rotation = Quaternion.Euler(0, cameraTarget.rotation.eulerAngles.y, 0);
        }*/
    }
}
