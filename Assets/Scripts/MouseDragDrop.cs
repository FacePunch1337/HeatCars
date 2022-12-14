using Photon.Pun.Demo.PunBasics;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using System.Drawing;

public class MouseDragDrop : MonoBehaviourPun
{
    public float forceAmount = 500;

    private Rigidbody selectedRigidbody;
    private Collider collider;
    public GameObject spawnObject;
    private Camera targetCamera;
    private Vector3 originalScreenTargetPosition;
    private Vector3 originalRigidbodyPos;
    private Vector3 colliderPos;
    private float selectionDistance;
    private float colliderDistance;
    private GameManager gm;
    


    void Start()
    {
        if (photonView.IsMine)
        {
            targetCamera = GameObject.Find("FollowCamera").GetComponent<Camera>();
        }
        GameObject.Find("GameManager").TryGetComponent(out GameManager gameManager);
        gm = gameManager;
        
        
    }

    void Update()
    {
        if (!targetCamera)
            return;

        if (Input.GetMouseButtonDown(0) && photonView.Owner.IsMasterClient)
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
       


    }

    void FixedUpdate()
    {
        if (selectedRigidbody)
        {
            
            if (gm.adminPanelOpen)
            {
                if (photonView.IsMine) { SendDragDropData(); }
            }

            else return;
        }
        /*if (collider)
        {
            if (gm.adminPanelOpen)
            {
                if (photonView.Owner.IsLocal) { SendSpawnData(Input.mousePosition); }
                


            }

        }*/



    }
    private Rigidbody GetRigidbodyFromMouseClick()
    {
        RaycastHit hitInfo = new RaycastHit();
        Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo);
        if (hit)
        {
            if (hitInfo.collider.gameObject.GetComponent<Rigidbody>() && photonView.Owner.IsLocal)
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
            if (hitInfo.collider.gameObject.GetComponent<Collider>() && photonView.Owner.IsLocal)
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
        Vector3 mousePositionOffset = targetCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, selectionDistance)) - originalScreenTargetPosition;
        selectedRigidbody.velocity = (originalRigidbodyPos + mousePositionOffset - selectedRigidbody.transform.position) * forceAmount * Time.deltaTime;
    }

    public void SendSpawnData(Vector3 spawnPos)
    {
        float[] arrPoints = { spawnPos.x, spawnPos.y + 15, colliderDistance};
        photonView.RPC("SpawnObject", RpcTarget.AllBuffered, arrPoints);
    }

    [PunRPC]
    public void SpawnObject(float[] spawnPoint)
    {
        
        Vector3 spawnPos = new Vector3(spawnPoint[0], spawnPoint[1], spawnPoint[2]);
       // Vector3 worldPos = Camera.main.ScreenToWorldPoint(spawnPos);
        Instantiate(spawnObject, spawnPos, Quaternion.identity);
        collider = null;
    }
}