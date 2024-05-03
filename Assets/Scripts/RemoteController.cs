using System;
using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Shared;
using UnityEngine;

public class RemoteController : MonoBehaviour
{
    [SerializeField] private Quadcopter quadcopter;

    private HVRGlobalInputs _input;

    private bool _hasLeftGrabbable;
    private bool _hasRightGrabbable;

    private void Start()
    {
        _input = HVRGlobalInputs.Instance;
        
        EndWindow.onOpenWindow += () => enabled = false;
    }

    public void ActivateInputs(HVRGrabberBase hvrGrabberBase, HVRGrabbable hvrGrabbable)
    {
        if (!enabled) return;
        
        SetInput(hvrGrabberBase, true);
    }
    
    public void DeactivateInputs(HVRGrabberBase hvrGrabberBase, HVRGrabbable hvrGrabbable)
    {
        if (!enabled) return;

        SetInput(hvrGrabberBase, false);
    }

    public void SetInput(HVRGrabberBase hvrGrabberBase, bool state)
    {
        if (hvrGrabberBase.GetType() == typeof(HVRHandGrabber))
        {
            HVRHandGrabber handGrabber = (HVRHandGrabber)hvrGrabberBase;
            
            Debug.Log(handGrabber.HandSide);
            switch (handGrabber.HandSide)
            {
                case HVRHandSide.Left:
                    _hasLeftGrabbable = state;
                    break;
                case HVRHandSide.Right:
                    _hasRightGrabbable = state;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
    private void Update()
    {
        if (_hasLeftGrabbable) quadcopter.LiftedYawVector = _input.LeftTrackpadAxis;
        if (_hasRightGrabbable) quadcopter.RollPitchVector = _input.RightTrackpadAxis;
    }
}
