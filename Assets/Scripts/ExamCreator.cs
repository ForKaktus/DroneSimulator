using UnityEngine;

public class ExamCreator : MonoBehaviour
{
    [SerializeField] private BuildTimeStopwatch buildStopwatch;
    [SerializeField] private TransportTimeStopwatch transportStopwatch;

    public void CreateExam() =>
        Nucleus.instance.CreateExam(buildStopwatch.time, transportStopwatch.time, Random.Range(1, 6), 0);
}