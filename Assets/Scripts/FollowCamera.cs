using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.SocialPlatforms;
using Photon.Realtime;
using System.Linq;
using UnityEngine.Playables;
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class FollowCamera : MonoBehaviour
{
    // public GameObject tPlayer;
    // public Transform tFollowTarget;
     //private CinemachineVirtualCamera vcam;
     private CinemachineFreeLook vcam;
     

     //private Transform player;
     public void Attach(Transform _transform)
     {
         vcam = GetComponent<CinemachineFreeLook>();

        GameObject.Find("GameManager").TryGetComponent(out SpawnPlayers spawnPlayers);
        var vcamRot = vcam.transform.rotation;
        vcamRot.x = 180;
         vcam.transform.parent = _transform;
         vcam.transform.position = Vector3.zero;
        vcam.transform.rotation = vcamRot;


    }

     // Use this for initialization
     void Start()
     {
        
     }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(vcam.transform.rotation);
        /*float aimX = Input.GetAxis("Mouse X");
        float aimY = Input.GetAxis("Mouse Y");
        vcam.transform.rotation *= Quaternion.AngleAxis(aimX * mouseSensitivity, Vector3.up);
        vcam.transform.rotation *= Quaternion.AngleAxis(-aimY * mouseSensitivity, Vector3.right);


        var angleX = vcam.LookAt.localEulerAngles.x;
        Debug.Log(angleX);
        if (angleX > 180 && angleX < maximumAngle)
        {
            angleX = maximumAngle;
        }
        else if (angleX < 180 && angleX > minimumAngle)
        {
            angleX = minimumAngle;
        }

        //vcam.LookAt.localEulerAngles = new Vector3(angleX, vcam.transform.parent.localEulerAngles.y, 0);*/

        vcam.LookAt = vcam.transform.parent;
        vcam.Follow = vcam.transform.parent;

        


    }
    /*public float MinDist, CurrentDist, MaxDist, TranslateSpeed, AngleH, AngleV;
    public Transform Target;
    private CinemachineVirtualCamera vcam;

    

    public void Update()
    {
        AngleH += Input.GetAxis("Mouse X");
        AngleV -= Input.GetAxis("Mouse Y");
        CurrentDist += Input.GetAxis("Mouse ScrollWheel");
    }

    public void FixedUpdate()
    {
        Vector3 tmp;
        tmp.x = (Mathf.Cos(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + vcam.transform.parent.position.x);
        tmp.z = (Mathf.Sin(AngleH * (Mathf.PI / 180)) * Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + vcam.transform.parent.position.z);
        tmp.y = Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + Target.position.y;
        vcam.Follow.position = Vector3.Slerp(vcam.transform.parent.position, tmp, TranslateSpeed * Time.deltaTime);
        vcam.LookAt.LookAt(vcam.transform.parent);
    }

    public void Attach(Transform _transform)
    {

        vcam = GetComponent<CinemachineVirtualCamera>();

        vcam.transform.parent = _transform;
        vcam.transform.position = Vector3.zero;
    }*/
}














