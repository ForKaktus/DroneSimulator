using UnityEngine;
using UnityEngine.Events;

public class Quadcopter : MonoBehaviour
{
    public UnityAction<Vector2, float> onPowerChange;
    public UnityAction<float> onAngularChange;

    private Vector2 _liftedYawVector;
    private Vector2 _rollPitchVector;

    public Rigidbody QuadcopterRigidbody { get; private set; }

    public Vector2 LiftedYawVector
    {
        get => _liftedYawVector;
        set
        {
            _liftedYawVector = value;
            
            onAngularChange.Invoke(LiftedYawVector.x);
            onPowerChange.Invoke(RollPitchVector, LiftedYawVector.y);
        }
    }

    public Vector2 RollPitchVector
    {
        get => _rollPitchVector;
        set
        {
            _rollPitchVector = value;
            
            onPowerChange.Invoke(RollPitchVector, LiftedYawVector.y);
        }
    }
    
    
    private void Awake()
    {
        QuadcopterRigidbody = GetComponent<Rigidbody>();

        EndWindow.onOpenWindow += () => enabled = false;
    }

    public void Update()
    {

        if (_rollPitchVector == Vector2.zero) ;
        
        Quaternion newRotation =
            Quaternion.Euler(_rollPitchVector.y * 25, transform.eulerAngles.y, -_rollPitchVector.x * 25);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 90f * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (!QuadcopterRigidbody) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(QuadcopterRigidbody.worldCenterOfMass, 0.05f);
    }
}
