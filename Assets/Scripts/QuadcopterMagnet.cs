using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class QuadcopterMagnet : MonoBehaviour
{
    public static Action OnPackageTaked;
    
    [SerializeField] private float magnetDistance;
    [SerializeField] private LayerMask packagesLayer;
    [SerializeField] private Vector3 offset;

    private Package _selectPackage;
    
    private void Start()
    {
        HVRControllerEvents.Instance.RightTriggerActivated.AddListener(MagnetInput);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            MagnetInput();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void MagnetInput()
    {
        if (_selectPackage)
        {
            _selectPackage.transform.SetParent(null);
            _selectPackage.Released();
            _selectPackage = null;
            StopAllCoroutines();
            return;
        }

        Ray ray = new Ray(transform.position, -transform.up);

        if (!Physics.Raycast(ray, out RaycastHit hit, magnetDistance, packagesLayer)) return;
        
        if (hit.transform.TryGetComponent(out Package package))
        {
            OnPackageTaked?.Invoke();
            _selectPackage = package;
            StartCoroutine(MovePackageToMagnet());
        }
    }

    private IEnumerator MovePackageToMagnet()
    {
        float distance = Vector3.Distance(transform.position, _selectPackage.transform.position);
        Transform packageTransform = _selectPackage.transform;

        packageTransform.SetParent(transform);
        _selectPackage.Lifted();

        while (packageTransform.position + offset != transform.position && _selectPackage)
        {
            packageTransform.rotation = 
                Quaternion.RotateTowards(packageTransform.rotation, transform.rotation, Time.deltaTime / distance);
            packageTransform.position =
                Vector3.MoveTowards(packageTransform.position, transform.position + offset, Time.deltaTime / distance);
            yield return null;
        }
    }
}
