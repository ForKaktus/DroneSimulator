using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using nsDB;
using TMPro;
using UnityEngine.Events;

public class Recomendations : MonoBehaviour
{
    public static UnityAction<IEnumerable<AdditiveCriterion>> onGetAdditiveCriteria;

    [SerializeField] private List<RecommendationField> recommendationFields;
    [SerializeField] private TextMeshProUGUI recommendationText;

    private void Awake()
    {
        UserSigning.OnSigned += NormalizeCriteria;

        recommendationFields = recommendationFields.OrderBy(field => field.threshold).ToList();
    }

    /// <summary>
    /// Нормализовать критерии
    /// </summary>
    public void NormalizeCriteria()
    {
        // 1 Критерий
        List<BuildTimeCriteria> usersBuildTime = Nucleus.instance.GetBuildTime();
        // 2 Критерий
        List<DeliveryTimeCriteria> usersDeliveryTime = Nucleus.instance.GetDeliveryTime();
        // 3 Критерий
        List<DeliveryTimeCriteria> usersAverageDeliveryTime = Nucleus.instance.GetDeliveryTime();
        // 4 Критерий
        List<BrokenProductCountCriteria> usersBrokenProductCount = Nucleus.instance.GetExamBrokenProductCount();
        // 5 Критерий
        List<AccuracyCriteria> usersAccuracy = Nucleus.instance.GetExamAccuracy();

        /*foreach (BuildTimeCriteria userBuildTime in usersBuildTime)
        {
            Debug.Log($"userID: {userBuildTime.user_id}; buildTime: {userBuildTime.time_build}");
        }
        foreach (DeliveryTimeCriteria userDeliveryTime in usersDeliveryTime)
        {
            Debug.Log($"userID: {userDeliveryTime.user_id}; deliveryTime: {userDeliveryTime.time_delivery}");
        }
        foreach (DeliveryTimeCriteria userAverageDeliveryTime in usersAverageDeliveryTime)
        {
            Debug.Log($"userID: {userAverageDeliveryTime.user_id}; averageDeliveryTime: {userAverageDeliveryTime.time_delivery}");
        }
        foreach (BrokenProductCountCriteria userBrokenProductCount in usersBrokenProductCount)
        {
            Debug.Log($"userID: {userBrokenProductCount.user_id}; brokenProductCount: {userBrokenProductCount.brokenProductCount}");
        }
        foreach (AccuracyCriteria userAccuracy in usersAccuracy)
        {
            Debug.Log($"userID: {userAccuracy.user_id}; accuracy: {userAccuracy.accuracy}");
        }*/

        float maxCriteria = usersBuildTime.Max(max => max.time_build);
        
        foreach (BuildTimeCriteria t in usersBuildTime)
        {
            t.time_build /= maxCriteria;
            t.time_build *= -0.5f;
        }
        
        maxCriteria = usersDeliveryTime.Max(max => max.time_delivery);

        foreach (DeliveryTimeCriteria user in usersDeliveryTime)
        {
            user.time_delivery /= maxCriteria;
            user.time_delivery *= -0.75f;
        }
        
        maxCriteria = usersAverageDeliveryTime.Max(max => max.time_delivery);

        foreach (DeliveryTimeCriteria user in usersAverageDeliveryTime)
        {
            user.time_delivery /= maxCriteria;
            user.time_delivery *= -0.5f;
        }
        
        maxCriteria = usersBrokenProductCount.Max(max => max.brokenProductCount);

        foreach (BrokenProductCountCriteria user in usersBrokenProductCount)
        {
            if (user.brokenProductCount == 0) continue;
            user.brokenProductCount = (int)(user.brokenProductCount / maxCriteria);
        }
        
        maxCriteria = usersAccuracy.Max(max => max.accuracy);

        foreach (AccuracyCriteria user in usersAccuracy)
        {
            user.accuracy /= maxCriteria;
            user.accuracy *= 0.25f;
        }

        List<User> users = Nucleus.instance.GetUsers();
        List<AdditiveCriterion> additiveCriteria = new();
        AdditiveCriterion additiveCriteriaActiveUser = null;

        for (int i = 0; i < users.Count; i++)
        {
            AdditiveCriterion additiveCriterion = new ();
            
            additiveCriterion.userId = users[i].user_id;
            
            
            try
            {
                additiveCriterion.additiveCriteria =
                    usersAccuracy.Single(e => e.user_id == users[i].user_id).accuracy +
                    usersBrokenProductCount.Single(e => e.user_id == users[i].user_id).brokenProductCount +
                    usersAverageDeliveryTime.Single(e => e.user_id == users[i].user_id).time_delivery +
                    usersDeliveryTime.Single(e => e.user_id == users[i].user_id).time_delivery +
                    usersBuildTime.Single(e => e.user_id == users[i].user_id).time_build;

                if (additiveCriterion.userId == Nucleus.currentUserId)
                {
                    additiveCriteriaActiveUser = additiveCriterion;
                }
            }
            catch (Exception e)
            {
                continue;
            }
            finally
            {
                additiveCriteria.Add(additiveCriterion);
            }
        }
        
        float sum = additiveCriteria.Sum(e => e.additiveCriteria);

        for (int index = 0; index < additiveCriteria.Count; index++)
        {
            AdditiveCriterion item = additiveCriteria[index];
            item.additiveCriteria /= sum;
        }

        onGetAdditiveCriteria?.Invoke(additiveCriteria);
        if (additiveCriteriaActiveUser != null)
        {
            SetRecommendation(additiveCriteriaActiveUser.additiveCriteria);   
        }
    }

    private void SetRecommendation(float additiveCriteria)
    {
        for (int i = 0; i < recommendationFields.Count; i++)
        {
            Debug.Log($"UserID: {Nucleus.currentUserId}; Additive: {additiveCriteria}");

            if (additiveCriteria <= recommendationFields[i].threshold)
            {
                recommendationText.text = recommendationFields[i].recommendationText;
                break;
            }
        }
    }

    public void ClearRecommendationText()
    {
        recommendationText.text = String.Empty;
    }
    
    [Serializable]
    struct RecommendationField
    {
        public string recommendationText;
        public float threshold;
    }
}
