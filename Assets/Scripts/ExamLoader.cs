using System.Collections.Generic;
using nsDB;
using UnityEngine;
using System.Linq;
using UnityEngine.Serialization;

public class ExamLoader : MonoBehaviour
{
    public static ExamLoader instance;
    
    [SerializeField] private GameObject examLayout;
    [SerializeField] private GameObject examUIPrefab;

    private List<Exam> _exams;

    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    /// <summary>
    /// Загрузить и отобразить экзамены
    /// </summary>
    public void LoadExams()
    {
        /*_exams = Nucleus.instance.GetExams();

        _exams = _exams.OrderByDescending(exam => exam.exam_accuracy).ToList();

        for (int index = 0; index < _exams.Count; index++)
        {
            Exam exam = _exams[index];
            Instantiate(examUIPrefab, examLayout.transform).TryGetComponent(out ExamUI examUI);
            examUI.UpdateUI(exam.exam_time_delivery, exam.exam_accuracy,
                Nucleus.instance.GetUserName(exam.exam_user_id));
            examUI.SetNumberText(index + 1);
        }*/
    }

    /// <summary>
    /// Удалить экзамен
    /// </summary>
    public void DeleteExam()
    {
        Nucleus.instance.DeleteExam();
    }
    
    /// <summary>
    /// Удалить UI экзаменов
    /// </summary>
    public void RemoveExam()
    {
        for (int i = 0; i < examLayout.transform.childCount; i++)
        {
            Destroy(examLayout.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Перезагрузить экзамен
    /// </summary>
    public void ReloadLabs()
    {
        RemoveExam();
        LoadExams();
    }
}