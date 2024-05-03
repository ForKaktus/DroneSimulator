using UnityEngine;

public class BuildTimeStopwatch : MonoBehaviour
{
    public float time;

    private bool _isRunning = true;

    private void Start()
    {
        QuadcopterMagnet.OnPackageTaked += StopStopwatch;
    }

    private void Update()
    {
        if (_isRunning == false) return;

        time += Time.deltaTime;
    }

    private void StopStopwatch() => _isRunning = false;
}
