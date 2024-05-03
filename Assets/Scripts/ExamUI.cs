using TMPro;
using UnityEngine;

public class ExamUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI gameTimeText;
    [SerializeField] private TextMeshProUGUI loginText;
    [SerializeField] private TextMeshProUGUI numberText;

    public void SetUI(float gameTime, float additive, string login, int number)
    {
        gameTimeText.text = additive.ToString();
        accuracyText.text = GameTime.FormatTime((int)gameTime).ToString("mm:ss");
        loginText.text = login;
        numberText.text = number.ToString();
    }
}
