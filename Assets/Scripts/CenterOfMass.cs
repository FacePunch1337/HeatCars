using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{
    public Vector3 _centerOfMass;
    public bool Awake;
    protected Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();


    }

    private void Update()
    {
        rigidbody.centerOfMass = _centerOfMass;
        rigidbody.WakeUp();
        Awake = !rigidbody.IsSleeping();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * _centerOfMass, 1f);
    }

}
