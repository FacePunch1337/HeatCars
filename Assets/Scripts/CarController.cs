using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;
using System.IO;
using UnityEngine.Experimental.Rendering.Universal;
using Photon.Realtime;
using JetBrains.Annotations;
using TMPro;
using ExitGames.Client.Photon.StructWrapping;
using ExitGames.Client.Photon;
using System.Xml.Serialization;
using UnityEngine.EventSystems;
using System.Data;
using UnityEngine.Experimental.GlobalIllumination;
using System;

//using System.Drawing;

//using static CarController;

[RequireComponent(typeof(Rigidbody))]

public class CarController : MonoBehaviourPun
{
    public TextMeshPro nickname { get { return _nickname; } set { _nickname = value; } }
   
    
    public List<AxleInfo> axleInfos;
    public Transform carTransform;
    public new Rigidbody rigidbody;
    public Vector3 _centerOfmass;


    public float maxMotorTorque;
    public float maxSteeringAngle;
    public bool ready;
    private PhotonView view;
    private TextMeshPro _nickname;
   
   //private Joystick joystick;
    private float motor;
    private float steering;
    private float _currentSpeed;
    private Collision collision;
    public float currentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
    private const byte COLOR_CHANGE_EVENT = 0;
    [SerializeField] private Material carBodyMaterial;
    [SerializeField] private Light[] spotLights;
    [SerializeField] public TextMeshPro myMessage;
    
    private bool lightOn;

    private GameManager gameManager;
    private ModManager modManager;
    private Chat chat;
    private SpawnPlayers spawn;
    RearWheelDrive rearWheelDrive;

    public void Start()
    {
        myMessage.text = string.Empty;
        lightOn = false;
        view = GetComponent<PhotonView>();
        nickname = GetComponentInChildren<TextMeshPro>();
        spotLights = GetComponents<Light>();
        //joystick = GameObject.Find("Dynamic Joystick").GetComponent<DynamicJoystick>();
        nickname.text = view.Owner.NickName;
        
        ready = false;

        GameObject.Find("GameManager").TryGetComponent(out GameManager _gameManager);
        GameObject.Find("ChatPanel").TryGetComponent(out Chat _chat);
        gameObject.TryGetComponent(out RearWheelDrive _rearWheelDrive);
        rearWheelDrive = _rearWheelDrive;
        //gameObject.TryGetComponent(out Message _message);
        gameManager = _gameManager;
        chat = _chat;
        //message = _message;
        

        gameManager.TryGetComponent(out ModManager _modManager);
        modManager = _modManager;

        gameManager.TryGetComponent(out SpawnPlayers _spawn);
        spawn = _spawn;


        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = _centerOfmass;
        

        //collision = GetComponent<Collision>();

    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }







    public void TireEffect()
    {
        

        foreach (var wheel in rearWheelDrive.wheels)
        {
            if (currentSpeed > 100)
            {
                rearWheelDrive.GetComponentInChildren<TrailRenderer>().emitting = true;

            }
            else rearWheelDrive.GetComponentInChildren<TrailRenderer>().emitting = false;
        }
    }
    public void FixedUpdate()
    {
        currentSpeed = rigidbody.velocity.magnitude;






        if (photonView.Owner.IsLocal)
        {
            SendRoomChat();
        }






        //Debug.Log(currentSpeed);
        if (view.IsMine && !chat.chatOpen)
        {
            //steering = (joystick.Vertical + joystick.Horizontal) * maxSteeringAngle;

            //motor = joystick.Vertical  * (maxSteeringAngle + maxMotorTorque) * joystick.Horizontal;
           
            motor = maxMotorTorque * Input.GetAxis("Vertical");
            steering = maxSteeringAngle * Input.GetAxis("Horizontal");


            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {

                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;

                   /* if (currentSpeed > 10f)
                    {
                        WheelFrictionCurve wheelFrictionCurve = new WheelFrictionCurve();

                        wheelFrictionCurve.extremumSlip = 3f;
                        wheelFrictionCurve.extremumValue = 3f;
                        wheelFrictionCurve.asymptoteSlip = 4f;
                        wheelFrictionCurve.asymptoteValue = 3f;
                        wheelFrictionCurve.stiffness = 5;




                        axleInfo.rightWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;
                        axleInfo.leftWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;
                    }

                    else
                    {*/
                       /* WheelFrictionCurve wheelFrictionCurve = new WheelFrictionCurve();

                        wheelFrictionCurve.extremumSlip = 0f;
                        wheelFrictionCurve.extremumValue = 2;
                        wheelFrictionCurve.asymptoteSlip = 0;
                        wheelFrictionCurve.asymptoteValue = 2;
                        wheelFrictionCurve.stiffness = 5;




                        axleInfo.rightWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;
                        axleInfo.leftWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;*/
                  //  }


                }
                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                    
                }





                if (Input.GetKey(KeyCode.Space))
                {
                    
                    
                    if (axleInfo.braking)
                    {
                        axleInfo.rightWheel.GetComponent<WheelCollider>().brakeTorque = 3000;
                        axleInfo.leftWheel.GetComponent<WheelCollider>().brakeTorque = 3000;

                       
                    }

                   WheelFrictionCurve wheelFrictionCurve = new WheelFrictionCurve();

                        wheelFrictionCurve.extremumSlip = 1f;
                        wheelFrictionCurve.extremumValue = 2f;
                        wheelFrictionCurve.asymptoteSlip = 2f;
                        wheelFrictionCurve.asymptoteValue = 2f;
                        wheelFrictionCurve.stiffness = 2f;



                        
                        axleInfo.rightWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;
                        axleInfo.leftWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;

                    axleInfo.rightWheel.GetComponent<WheelCollider>().suspensionDistance = 0.1f;
                    axleInfo.leftWheel.GetComponent<WheelCollider>().suspensionDistance = 0.1f;

                    //  TireEffect();



                }
               
                else 
                {
                    axleInfo.rightWheel.GetComponent<WheelCollider>().brakeTorque = 0;
                    axleInfo.leftWheel.GetComponent<WheelCollider>().brakeTorque = 0;
                    
                        WheelFrictionCurve wheelFrictionCurve = new WheelFrictionCurve();

                    wheelFrictionCurve.extremumSlip = 1f;
                    wheelFrictionCurve.extremumValue = 2f;
                    wheelFrictionCurve.asymptoteSlip = 2f;
                    wheelFrictionCurve.asymptoteValue = 2f;
                    wheelFrictionCurve.stiffness = 3f;






                    axleInfo.rightWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;
                        axleInfo.leftWheel.GetComponent<WheelCollider>().sidewaysFriction = wheelFrictionCurve;

                        axleInfo.rightWheel.GetComponent<WheelCollider>().suspensionDistance = 0.2f;
                        axleInfo.leftWheel.GetComponent<WheelCollider>().suspensionDistance = 0.2f;

                     

                }


                
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (!modManager.startMod && !chat.chatOpen)
                    {
                        var randomSpawnPos = spawn.spawnPoints[UnityEngine.Random.Range(0, spawn.spawnPoints.Length)].transform;
                        float[] posCoords = { randomSpawnPos.position[0], randomSpawnPos.position[1], randomSpawnPos.position[2]};
             
                        Teleport(posCoords);
                        //gameObject.GetComponent<Transform>().Rotate(0, randomSpawnPos.rotation.y, 0);
                    }
                    else return;
                    
                    // SendCustomCarDate();
                    // ChangeColor();
                }
                
               

                if (Input.GetKey(KeyCode.L))
                {
                   // LightOnOff();
                   


                    // SendCustomCarDate();
                    // ChangeColor();
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    //SendTeleport();
                }

                ApplyLocalPositionToVisuals(axleInfo.leftWheel);
                ApplyLocalPositionToVisuals(axleInfo.rightWheel);

               

            }

           
        }
        if (modManager.teleportToMod)
        {

            TeleportTo();
            
            modManager.teleportToMod = false;

        }
        if (modManager.teleportToSpawn)
        {

            TeleportTo();
            
            modManager.teleportToSpawn = false;

        }
        //else return;
    }



    public void SendRoomChat()
    {
        if (chat.chatMessage != null)
        { 
            
                photonView.RPC("RoomChat", RpcTarget.All, chat.chatMessage);
            
        }
        //else Invoke("SendClearMessage", 3);
   
    }
    [PunRPC]
    public void RoomChat(string _message)
    {

        myMessage.text = _message;
        


    }
    
    private void SendClearMessage()
    {
        
       
        photonView.RPC("ClearMessage", RpcTarget.All, string.Empty);
       
      
    }
    [PunRPC]
    public void ClearMessage(string empty)
    {
        myMessage.text = empty;
    }

    public void LightOnOff()
    {
        if(lightOn)
        {
            spotLights[0].intensity = 0;
            spotLights[1].intensity = 0;
            lightOn = false;
        }
        else
        {
            spotLights[0].intensity = 1000;
            spotLights[1].intensity = 1000;
            lightOn = true;
        }
        
    }


    

    public void TeleportTo()
    {
        Vector3 randomSpawnPos;
        if (modManager.teleportToMod)
        {
            switch (modManager)
            {
                default:
                    break;
            }
            randomSpawnPos = modManager.spawnPointsKOB[UnityEngine.Random.Range(0, modManager.spawnPointsKOB.Length)].transform.position;
            SendTeleport(randomSpawnPos);
            
        }

        

        if (modManager.teleportToSpawn)
        {
            randomSpawnPos = spawn.spawnPoints[UnityEngine.Random.Range(0, spawn.spawnPoints.Length)].transform.position;
            SendTeleport(randomSpawnPos);
            
        }
        

    }
    public void SendTeleport(Vector3 randomSpawnPos)
    {

        float[] points = { randomSpawnPos[0], randomSpawnPos[1], randomSpawnPos[2] };
        photonView.RPC("Teleport", RpcTarget.All, points);

    }

    /*public void SendTeleport(SpawnPlayers _spawn)
    {
        _spawn = spawn;
        
        var randomSpawnPos = _spawn.spawnPoints[Random.Range(0, _spawn.spawnPoints.Length)].transform.position;
        float[] points = { randomSpawnPos.x, randomSpawnPos.y, randomSpawnPos.z };
        photonView.RPC("Teleport", RpcTarget.All, points);

    }*/

    [PunRPC]   
    public void Teleport(float[] randomSpawnPos)
    {
        
        transform.position = new Vector3(randomSpawnPos[0], randomSpawnPos[1], randomSpawnPos[2]);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        rigidbody.velocity = new Vector3(0, 0, 0);
        //gameObject.GetComponent<Transform>().Rotate(0, 213, 0);
       

    }

    



    private void OnCollisionEnter(Collision _collision)
    {
       /* if (_collision.gameObject.CompareTag("Car"))
        {

           // gameObject.GetComponent<Rigidbody>().AddForce(rigidbody.velocity * 6, ForceMode.Impulse);

        }*/

    }

   



    /*private void NetworkingClient_EventRecived(EventData obj)
    {
        if (obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] datas = (object[])obj.CustomData;
            float r = (float)datas[0];
            float g = (float)datas[1];
            float b = (float)datas[2];

            var carMaterialList = gameObject.GetComponent<MeshRenderer>();
            carMaterialList.materials[0].color = new Color(r, g, b);
        
        }
    }

   /* public void ChangeColor()
    {
        var color = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        float r = color.r;
        float g = color.g;
        float b = color.b;

        var carMaterialList = gameObject.GetComponent<MeshRenderer>();
        carMaterialList.materials[0].color = new Color(r, g, b);

        object[] datas = new object[] { r, g, b };
        
        PhotonNetwork.RaiseEvent(COLOR_CHANGE_EVENT, datas, RaiseEventOptions.Default, SendOptions.SendReliable);

    }*/






}





[System.Serializable]
public class AxleInfo
{

    public WheelCollider leftWheel;
    public GameObject leftWheelVisuals;
    private bool leftGrounded = false;
    private float travelL = 0f;
    private float leftAckermanCorrectionAngle = 0;
    public WheelCollider rightWheel;
    public GameObject rightWheelVisuals;
    public float suspDist;

    //public GameObject wheelEffectsObj;

    private bool rightGrounded = false;
    private PhotonView view;
    private CarController carController;
    private float travelR = 0f;
    private float rightAckermanCorrectionAngle = 0;
    public bool motor;
    public bool steering;
    public bool braking;


    public float Antiroll = 10000;
    private float AntrollForce = 0;

    public float ackermanSteering = 1f;
    public void ApplyLocalPositionToVisuals()
    {

        //left wheel
        if (leftWheelVisuals == null)
        {
            return;
        }
        Vector3 position;
        Quaternion rotation;
        leftWheel.GetWorldPose(out position, out rotation);

        leftWheelVisuals.transform.position = position;
        leftWheelVisuals.transform.rotation = rotation;

        //right wheel
        if (rightWheelVisuals == null)
        {
            return;
        }
        rightWheel.GetWorldPose(out position, out rotation);

        rightWheelVisuals.transform.position = position;
        rightWheelVisuals.transform.rotation = rotation;

    }
    public void CalculateAndApplyAntiRollForce(Rigidbody theBody)
    {
        WheelHit hit;

        leftGrounded = leftWheel.GetGroundHit(out hit);
        if (leftGrounded)
            travelL = (-leftWheel.transform.InverseTransformPoint(hit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;
        else
            travelL = 1f;

        rightGrounded = rightWheel.GetGroundHit(out hit);
        if (rightGrounded)
            travelR = (-rightWheel.transform.InverseTransformPoint(hit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;
        else
            travelR = 1f;

        AntrollForce = (travelL - travelR) * Antiroll;

        if (leftGrounded)
            theBody.AddForceAtPosition(leftWheel.transform.up * -AntrollForce, leftWheel.transform.position);
        if (rightGrounded)
            theBody.AddForceAtPosition(rightWheel.transform.up * AntrollForce, rightWheel.transform.position);

    }
    public void CalculateAndApplySteering(float input, float maxSteerAngle, List<AxleInfo> allAxles)
    {

        
        //first find farest axle, we got to apply default values
        AxleInfo farestAxle = allAxles[0];
        //calculate start point for checking
        float farestAxleDistantion = ((allAxles[0].leftWheel.transform.localPosition - allAxles[0].rightWheel.transform.localPosition) / 2f).z;
        for (int a = 0; a < allAxles.Count; a++)
        {
            float theDistance = ((allAxles[a].leftWheel.transform.localPosition - allAxles[a].rightWheel.transform.localPosition) / 2f).z;
            // if we found axle that farer - save it
            if (theDistance < farestAxleDistantion)
            {
                farestAxleDistantion = theDistance;
                farestAxle = allAxles[a];
            }
        }
        float wheelBaseWidth = (Mathf.Abs(leftWheel.transform.localPosition.x) + Mathf.Abs(rightWheel.transform.localPosition.x)) / 2;
        float wheelBaseLength = Mathf.Abs(((farestAxle.leftWheel.transform.localPosition + farestAxle.rightWheel.transform.localPosition) / 2f).z) +
            Mathf.Abs(((leftWheel.transform.localPosition + rightWheel.transform.localPosition) / 2f).z);

        float angle = maxSteerAngle * input;
        //ackerman implementation
        float turnRadius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(angle))));
        if (input != 0)
        {
            //right wheel
            if (angle > 0)
            {//turn right

                rightAckermanCorrectionAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (turnRadius - wheelBaseWidth / 2f));
                rightAckermanCorrectionAngle = (rightAckermanCorrectionAngle - Mathf.Abs(angle)) * ackermanSteering + (Mathf.Abs(angle));
                rightAckermanCorrectionAngle = Mathf.Sign(angle) * rightAckermanCorrectionAngle;
            }
            else
            {//turn left

                rightAckermanCorrectionAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (turnRadius + wheelBaseWidth / 2f));
                rightAckermanCorrectionAngle = (rightAckermanCorrectionAngle - Mathf.Abs(angle)) * ackermanSteering + (Mathf.Abs(angle));
                rightAckermanCorrectionAngle = Mathf.Sign(angle) * rightAckermanCorrectionAngle;
            }
            //left wheel
            if (angle > 0)
            {//turn right
                leftAckermanCorrectionAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (turnRadius + wheelBaseWidth / 2f));
                leftAckermanCorrectionAngle = (leftAckermanCorrectionAngle - Mathf.Abs(angle)) * ackermanSteering + (Mathf.Abs(angle));
                leftAckermanCorrectionAngle = Mathf.Sign(angle) * leftAckermanCorrectionAngle;
            }
            else
            {//turn left
                leftAckermanCorrectionAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (turnRadius - wheelBaseWidth / 2f));
                leftAckermanCorrectionAngle = (leftAckermanCorrectionAngle - Mathf.Abs(angle)) * ackermanSteering + (Mathf.Abs(angle));
                leftAckermanCorrectionAngle = Mathf.Sign(angle) * leftAckermanCorrectionAngle;
            }
        }
        else
        {
            rightAckermanCorrectionAngle = 0f;
            leftAckermanCorrectionAngle = 0f;
        }
        leftWheel.steerAngle = leftAckermanCorrectionAngle;
        rightWheel.steerAngle = rightAckermanCorrectionAngle;
        Debug.Log(leftAckermanCorrectionAngle + " " + rightAckermanCorrectionAngle);
    }
}
      
    
           



    



