using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using nsDB;
using UnityEngine;

public class Rating : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject userPanelPrefab;

    [SerializeField] private UserSigning userSigning;
    
    public List<AdditiveCriterion> additiveCriteria;

    private void Awake()
    {
        Recomendations.onGetAdditiveCriteria += GetAdditiveCriteria;
    }

    public void ViewRating()
    {
        if(content && content.childCount > 0) return;

        additiveCriteria = additiveCriteria.OrderByDescending(criterion => criterion.additiveCriteria).ToList();

        List<User> users = Nucleus.instance.GetUsers();

        for (int i = 0; i < additiveCriteria.Count; i++)
        {
            User user = users.Single(e => e.user_id == additiveCriteria[i].userId);

            float  gameTime ;
            float  additive ;
            string login    ;
            int    number   ;
            try
            {
                Exam userExam = Nucleus.instance.GetExamByUserId(user.user_id);
                gameTime = userExam.exam_time_build + userExam.exam_time_delivery;
                additive = (float)Math.Round(additiveCriteria[i].additiveCriteria, 2);
                login = user.user_login;
                number = i + 1;
            }
            catch (Exception e)
            {
                continue;
            }
            
            ExamUI newUserPanel = Instantiate(userPanelPrefab.gameObject, content).GetComponent<ExamUI>();
            newUserPanel.SetUI(gameTime, additive, login, number);
        }
    }

    public void RemoveRatings()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
    
    /// <summary>
    /// Удалить экзамен
    /// </summary>
    public void DeleteExam()
    {
        Nucleus.instance.DeleteExam();
        
        // ДО СВЯЗИ)))
        userSigning.SignOut();
    }
    
    private void GetAdditiveCriteria(IEnumerable<AdditiveCriterion> additiveCriterion)
    {
        additiveCriteria = additiveCriterion.ToList();
        ViewRating();
    }
}
