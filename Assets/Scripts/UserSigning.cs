using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UserSigning : MonoBehaviour
{
    public static Action OnSigned;
    
    [Header("Registration")]
    [SerializeField] private TMP_InputField regNameField;
    [SerializeField] private TMP_InputField regPasswordField;

    [Header("Authorization")]
    [SerializeField] private TMP_InputField authNameField;
    [SerializeField] private TMP_InputField authPasswordField;

    [Header("UI")] 
    [SerializeField] private GameObject entryMenu;
    [SerializeField] private GameObject authorizationMenu;
    [SerializeField] private GameObject registrationMenu;
    [SerializeField] private GameObject mainMenu;
    
    [SerializeField] private Animator loginErrorAnim;
    [SerializeField] private Animator registerErrorAnim;

    [SerializeField] private Rating rating;
    [SerializeField] private Recomendations recommendations;

    private Nucleus _nucleus;
    
    private void Start()
    {
        _nucleus = Nucleus.instance;
        
        CheckUserLogin();
    }

    public void AuthorizeUser()
    {
        if (_nucleus.AuthorizeUser(authNameField.text, authPasswordField.text))
        {
            authorizationMenu.SetActive(false);
            SignIn();
        }
        else
        {
            loginErrorAnim.SetTrigger("Error");
            Debug.LogError("Authorization Error!");
        }
    }

    public void RegisterUser()
    {
        if (_nucleus.CreateUser(regNameField.text, regPasswordField.text))
        {
            registrationMenu.SetActive(false);
            SignIn();
        }
        else
        {
            registerErrorAnim.SetTrigger("Error");
            Debug.LogError("Registration Error!");
        }
    }

    public void SignOut()
    {
        Nucleus.currentUserId = -1;

        rating.RemoveRatings();
        recommendations.ClearRecommendationText();
        EnableEntry();
    }

    private void SignIn()
    {
        authNameField.text = string.Empty;
        authPasswordField.text = string.Empty;
        
        regNameField.text = string.Empty;
        regPasswordField.text = string.Empty;

        EnableMenu();

        OnSigned?.Invoke();
    }
    
    private void EnableMenu()
    {
        entryMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void EnableEntry()
    {
        mainMenu.SetActive(false);
        entryMenu.SetActive(true);
    }

    private void CheckUserLogin()
    {
        if (Nucleus.instance.CheckAuthorization() == false) return;
        
        SignIn();
    }
}