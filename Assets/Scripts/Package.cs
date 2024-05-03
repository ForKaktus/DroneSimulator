using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Package : MonoBehaviour
{
    public static Action OnDelivery;
    
    [SerializeField] private Transform deliveryPosition;
    [SerializeField] private Rigidbody packageRigidbody;
    [SerializeField] private EndWindow endWindow;

    public Transform DeliveryPosition => deliveryPosition;

    public void Lifted()
    {
        Destroy(packageRigidbody);
    }

    public void Released()
    {
        packageRigidbody = transform.AddComponent<Rigidbody>();

        if (Vector3.Distance(transform.position, deliveryPosition.position) < 1f)
        {
            OnDelivery?.Invoke();
            endWindow.ActivateWindow();
        }
    }
    
    
}
