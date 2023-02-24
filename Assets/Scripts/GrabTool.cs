using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTool : MonoBehaviour
{
    // Amount of force you want to apply to the object
    public float forceAmount = 500;
    // Minimal Scroll distance
    public float minimalDistance = 0;
    // Selected body(kinda useless variable, actually)
    Rigidbody selectedRigidbody;
    // Grab sphere, which used to grab object 
    GameObject grabSphere;
    // Material of the grab sphere
    public Material grabMateraial;
    // Distance from the camera to the grab sphere
    float dragDistance;
    // Previous position of the grab sphere
    Vector3 prevPos;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedRigidbody = TakeGigidbodyIfExists();
            // Save previous position
            prevPos = selectedRigidbody.position;
        }
        if (!Input.GetMouseButton(0) && selectedRigidbody)
        {

            selectedRigidbody = null;
            Destroy(grabSphere);
        }
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            dragDistance += Input.mouseScrollDelta.y;
            if (dragDistance < minimalDistance)
            {
                dragDistance = minimalDistance;
            }
        }
    }

    void FixedUpdate()
    {
        if (!selectedRigidbody)
        {
            return;
        }
        // Make a ray and find a point where body should go
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 desired_point = ray.GetPoint(dragDistance);
        // Add force to the grab sphere
        grabSphere.GetComponent<Rigidbody>().AddForce((desired_point - grabSphere.transform.position) * forceAmount * Time.deltaTime);
        // Some debug
        Debug.DrawLine(grabSphere.transform.position, desired_point, Color.black, Time.deltaTime, false);

    }

    Rigidbody TakeGigidbodyIfExists()
    // Add a new grab sphere to the object 
    {
        // Raycast from the screen to the object
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // If the object hit
            if (hit.collider.gameObject == this.gameObject)
            {
                // Create a new sphere
                grabSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // Disable the sphere's collision
                grabSphere.GetComponent<SphereCollider>().enabled = false;
                // Make the sphere smaller
                grabSphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                // Set the sphere's position to hit position
                grabSphere.transform.position = ray.GetPoint(hit.distance);
                // Set the sphere's material to the grab material
                grabSphere.GetComponent<MeshRenderer>().material = grabMateraial;
                // Add a new Rigidbody component to the sphere and make it weightless
                Rigidbody grabRigidbody = grabSphere.AddComponent(typeof(Rigidbody)) as Rigidbody;
                grabRigidbody.mass = 0f;
                // Add a new FixedJoint to the sphere and connect it to the object
                FixedJoint grabJoint = grabSphere.AddComponent(typeof(FixedJoint)) as FixedJoint;
                grabJoint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
                // Save grab distance
                dragDistance = hit.distance;
                // Return selected object
                return hit.collider.gameObject.GetComponent<Rigidbody>();
            }
        }
        return null;
    }
}
