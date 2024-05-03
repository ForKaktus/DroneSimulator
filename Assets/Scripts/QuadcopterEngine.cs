using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadcopterEngine : MonoBehaviour
{
    public bool isActive = true;

    [SerializeField] private Quadcopter quadcopter;
    [SerializeField] private Vector2 angularMomentumRange;
    [SerializeField] float liftingForceOnKilogram;
    [SerializeField] private AnimationCurve enginePowerCurve;

    private float _liftingForce;
    private float _angularMomentum;
    private Rigidbody _quadcopterRigidbody;

    private void Awake()
    {
        _angularMomentum = 0;
        _liftingForce = 0;
    }

    private void Start()
    {
        quadcopter.onPowerChange += UpdateForce;
        quadcopter.onAngularChange += UpdateAngular;
        _quadcopterRigidbody = quadcopter.QuadcopterRigidbody;
    }

    public void FixedUpdate()
    {
        if(!isActive) return;

        _quadcopterRigidbody.AddForceAtPosition(transform.up * _liftingForce, transform.position);
        _quadcopterRigidbody.AddRelativeTorque(transform.up * _angularMomentum);
    }

    private void UpdateForce(Vector2 rollPitchVector, float force)
    {
        _liftingForce = force;
        _liftingForce = enginePowerCurve.Evaluate(_liftingForce);
        _liftingForce = Mathf.Lerp(0, liftingForceOnKilogram * quadcopter.QuadcopterRigidbody.mass, _liftingForce);
    }

    private void UpdateAngular(float force)
    {
        if (Mathf.Abs(force) < 0.3f)
        {
            _angularMomentum = 0;
            return;
        }
        
        _angularMomentum = Mathf.InverseLerp(-1, 1, force);
        _angularMomentum = Mathf.Lerp(angularMomentumRange.x, angularMomentumRange.y, _angularMomentum);
    }
}
