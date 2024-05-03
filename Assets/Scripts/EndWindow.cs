using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EndWindow : MonoBehaviour
{
    public static UnityAction onOpenWindow;
    
    [SerializeField] private Transform targetCamera;
    [SerializeField] private ExamCreator examCreator;

    public void ActivateWindow()
    {
        gameObject.SetActive(true);
        examCreator.CreateExam();

        Vector3 viewPosition = targetCamera.forward;
        viewPosition.y = 0;

        transform.position = targetCamera.position + viewPosition;
        transform.rotation = Quaternion.LookRotation(-viewPosition) * Quaternion.Euler(0, 180, 0);
        
        onOpenWindow.Invoke();
    }
}
