using System;
using System.Collections.Generic;
using nsDB;
using SQLite4Unity3d;
using UnityEngine;
using Random = UnityEngine.Random;

public class Nucleus : MonoBehaviour
{
    public static Nucleus instance;
    
    public static int currentUserId = -1;

    private SQLiteConnection _db = DB.database.getConn();
    
    private void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
        
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    /// <param name="userName">Имя преподавателя</param>
    /// <param name="userPassword">Пароль преподавателя</param>
    public bool CreateUser(string userName, string userPassword)
    {
        try
        {
            User newUser = new();
            newUser.user_login = userName; 
            newUser.user_password = userPassword;
            newUser.user_role_id = 0;
        
            _db.Insert(newUser);
        
            currentUserId = newUser.user_id;

            return true;
        }
        catch (SQLiteException e)
        {
            Debug.LogException(e);
            return false;
        }
    }

    /// <summary>
    /// Авторизовать пользователя
    /// </summary>
    /// <param name="userName">Имя пользователя</param>
    /// <param name="userPassword">Пароль пользователя</param>
    /// <returns>True при успешной атворизации</returns>
    public bool AuthorizeUser(string userName, string userPassword)
    {
        List<User> records = _db.Query<User>("select * from users where user_login = ? and user_password = ?", new object[] {userName, userPassword});
        
        if (records.Count == 0) return false;

        currentUserId = records[0].user_id;
        return true;
    }


    public bool CreateExam(float buildTime, float deliveryTime, int examAccuracy, int brokeProductsCount)
    {
        try
        {
            Exam exam = GetExamByUserId();

            if (exam == null)
            {
                Exam newExam = new ()
                {
                    exam_user_id = currentUserId,
                    exam_accuracy = examAccuracy,
                    exam_broke_product = brokeProductsCount,
                    exam_time_build = buildTime,
                    exam_time_delivery = deliveryTime
                };    
                _db.Insert(newExam);
            }
            else
            {
                exam.exam_user_id = currentUserId;
                exam.exam_accuracy = examAccuracy;
                exam.exam_broke_product = brokeProductsCount;
                exam.exam_time_build = buildTime;
                exam.exam_time_delivery = deliveryTime;
                _db.Update(exam);
            }
            
            _db.Commit();

            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return false;
        }
    }

    /// <summary>
    /// Удалить экзамен
    /// </summary>
    /// <param name="labId"></param>
    /// <returns></returns>
    public bool DeleteExam()
    {
        try
        {
            List<Exam> records = _db.Query<Exam>("select * from exams where exam_user_id = ?", currentUserId);

            _db.Delete(records[0]);
            _db.Commit();
            
            return true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);

            return false;
        }
    }
    
    /// <summary>
    /// Получить все экзамены
    /// </summary>
    /// <returns>Список из экзаменов</returns>
    public List<Exam> GetExams()
    {
        List<Exam> records = _db.Query<Exam>("select * from exams");

        return records;
    }

    /// <summary>
    /// Получить экзамен по ID
    /// </summary>
    /// <returns>Объект экзамена</returns>
    public Exam GetExamByUserId()
    {
        List<Exam> records = _db.Query<Exam>("select * from exams where exam_user_id = ?", currentUserId);

        return records.Count == 0 ? null : records[0];
    }
    
    public Exam GetExamByUserId(int userId)
    {
        List<Exam> records = _db.Query<Exam>("select * from exams where exam_user_id = ?", userId);

        return records.Count == 0 ? null : records[0];
    }
    
    /// <summary>
    /// Получить имя пользователя
    /// </summary>
    /// <returns>Имя пользователя</returns>
    public string GetUserName(int userId)
    {
        List<User> records = _db.Query<User>("select * from users where user_id = ?", new object[] { userId });

        return records[0].user_login;
    }

    public List<BuildTimeCriteria> GetBuildTime()
    {
        List<User> users = GetUsers();
        List<BuildTimeCriteria> usersBuildTime = new();

        foreach (User user in users)
        {
            List<Exam> exams = _db.Query<Exam>("select * from exams where exam_user_id = ?", user.user_id);

            if (exams.Count == 0) continue;

            BuildTimeCriteria buildTimeCriteria = new()
            {
                user_id = user.user_id,
                time_build = exams[0].exam_time_build
            };

            usersBuildTime.Add(buildTimeCriteria);
        }

        return usersBuildTime;
    }
    
    public List<DeliveryTimeCriteria> GetDeliveryTime()
    {
        List<User> users = GetUsers();
        List<DeliveryTimeCriteria> usersDeliveryTime = new();

        foreach (User user in users)
        {
            List<Exam> exams = _db.Query<Exam>("select * from exams where exam_user_id = ?", user.user_id);

            if (exams.Count == 0) continue;

            DeliveryTimeCriteria deliveryTimeCriteria = new()
            {
                user_id = user.user_id,
                time_delivery = exams[0].exam_time_delivery
            };

            usersDeliveryTime.Add(deliveryTimeCriteria);
        }

        return usersDeliveryTime;
    }
    
    public List<AccuracyCriteria> GetExamAccuracy()
    {
        List<User> users = GetUsers();
        List<AccuracyCriteria> usersAccuracy = new();

        foreach (User user in users)
        {
            List<Exam> exams = _db.Query<Exam>("select * from exams where exam_user_id = ?", user.user_id);

            if (exams.Count == 0) continue;

            AccuracyCriteria accuracyCriteria = new()
            {
                user_id = user.user_id,
                accuracy = exams[0].exam_accuracy
            };

            usersAccuracy.Add(accuracyCriteria);
        }

        return usersAccuracy;
    }
    
    public List<BrokenProductCountCriteria> GetExamBrokenProductCount()
    {
        List<User> users = GetUsers();
        List<BrokenProductCountCriteria> usersBrokenProductCriteria = new();

        foreach (User user in users)
        {
            List<Exam> exams = _db.Query<Exam>("select * from exams where exam_user_id = ?", user.user_id);

            if (exams.Count == 0) continue;

            BrokenProductCountCriteria brokenProductCountCriteria = new()
            {
                user_id = user.user_id,
                brokenProductCount = exams[0].exam_broke_product
            };

            usersBrokenProductCriteria.Add(brokenProductCountCriteria);
        }

        return usersBrokenProductCriteria;
    }
    
    public bool CheckAuthorization()
    {
        return currentUserId >= 0;
    }

    public List<User> GetUsers()
    {
        List<User> records = _db.Query<User>("select * from users");
        return records;
    }
}