using UnityEngine;

public class TransportTimeStopwatch : MonoBehaviour
{
    public float time;

    private bool _isRunning;

    private void Start()
    {
        Package.OnDelivery += StopStopwatch;
        QuadcopterMagnet.OnPackageTaked += StartStopwatch;
    }

    private void Update()
    {
        if (_isRunning == false) return;

        time += Time.deltaTime;
    }
    
    private void StartStopwatch() => _isRunning = true;
    private void StopStopwatch() => _isRunning = false;
}
