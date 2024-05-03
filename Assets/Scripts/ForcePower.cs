using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePower : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float force;
    [SerializeField] private Rigidbody objectRigidbody;

    private void Start()
    {
        force = Physics.gravity.magnitude;
    }

    private void FixedUpdate()
    {
        objectRigidbody.AddForce(direction.normalized * force, ForceMode.Force);
    }
}
