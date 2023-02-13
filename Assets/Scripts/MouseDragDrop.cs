using Photon.Pun.Demo.PunBasics;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using System.Drawing;

public class MouseDragDrop : MonoBehaviourPun
{
    public float forceAmount = 500;

    private Rigidbody selectedRigidbody;
    private new Collider collider;
    
    private Camera targetCamera;
    private Vector3 originalScreenTargetPosition;
    private Vector3 originalRigidbodyPos;
    private Vector3 colliderPos;
    private float selectionDistance;
    private float colliderDistance;
    private GameManager gm;
    private GameProps gameProps;
    

    private int PropID;
    
   


    void Start()
    {
        if (photonView.IsMine)
        {
            targetCamera = GameObject.Find("FollowCamera").GetComponent<Camera>();
        }
        
        
        
        GameObject.Find("GameManager").TryGetComponent(out GameManager gameManager);
        gm = gameManager;
        GameObject.Find("GameManager").TryGetComponent(out GameProps _gameProps);
        gameProps = _gameProps;
        



    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidbody = GetRigidbodyFromMouseClick();
        }
        if (Input.GetMouseButtonUp(0) && selectedRigidbody)
        {
            selectedRigidbody = null;
        }
        if (Input.GetMouseButtonDown(1))
        {
            collider = GetFlatToSpawn();
        }
        if (Input.GetMouseButtonUp(1))
        {
            collider = null;
        }



    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            
            if (gm.adminPanelOpen)
            {
                SendDragDropData();
            }

            else return;
        }
        if (collider)
        {
            if (gm.adminPanelOpen)
            {
                SendSpawnData(Input.mousePosition);
            }

        }



    }
    private Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>())
            {
                selectionDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                originalRigidbodyPos = hitInfo.collider.transform.position;
                
                return hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            }
        }

        return null;
    }

    /*public void SendFlatToSpawnData(Vector3 colliderDistance, Vector3 originalScreenTargetPosition, Vector3 colliderPos)
    {
        Vector3[] spawnData = { colliderDistance, originalScreenTargetPosition, colliderPos};
        photonView.RPC("SpawnObject", RpcTarget.AllBuffered, spawnData);
    }*/

    
    private Collider GetFlatToSpawn()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Collider>())
            {

                colliderDistance = Vector3.Distance(ray.origin, hitInfo.point);
                originalScreenTargetPosition = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance));
                colliderPos = hitInfo.collider.transform.position;
                
                return hitInfo.collider.gameObject.GetComponent<Collider>();
            }
        }

        return null;
    }

    public void SendDragDropData()
    {
        photonView.RPC("DragDrop", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void DragDrop()
    {
        Vector3 temp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance);
        //Debug.Log(targetCamera);
        Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(temp);
        var a = mousePositionOffset - originalScreenTargetPosition;
        selectedRigidbody.velocity = (originalRigidbodyPos + a - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
    }

    public void SendSpawnData(Vector3 spawnPos)
    {
        PropID = gameProps.propID;
        float[] arrPoints = { spawnPos.x, spawnPos.y, colliderDistance};
        photonView.RPC("SpawnObject", RpcTarget.AllBuffered, arrPoints);
        
    }

    [PunRPC]
    public void SpawnObject(float[] spawnPoint)
    {

        
        Vector3 spawnPos = new Vector3(spawnPoint[0], spawnPoint[1] + 15, spawnPoint[2]);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        Instantiate(gameProps._props[PropID], worldPos, Quaternion.identity);
        //Debug.Log(gameProps._props[PropID]);
        collider = null;
    }
}