using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private TextMeshProUGUI textHP;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private GameObject panel_gameEnd;
    [SerializeField] private TextMeshProUGUI textEnd_Title;
    [SerializeField] private TextMeshProUGUI textEnd_TotalScore;
    [SerializeField] private GameObject panel_gameFinish;
    [SerializeField] private TextMeshProUGUI textFinish_HP;
    [SerializeField] private TextMeshProUGUI textFinish_Score;
    [SerializeField] private TextMeshProUGUI textFinish_Time;
    [SerializeField] private GameObject panel_gameOver;
    [SerializeField] private TextMeshProUGUI textGameOver_Cause;

    public void UpdateUI()
    {
        textHP.text = "HP: x" + PlayerData.HP;
        textScore.text = "Score: " + PlayerData.SCORE;
    }

    public void UpdateTimeUI()
    {
        textTime.text = "Time: " + PlayerData.TIME;
    }

    public void ShowPanel(string panel_type)
    {
        panel_gameEnd.gameObject.SetActive(true);
        LevelController.Instance.LevelEnd();

        switch (panel_type)
        {
            case "GAME_FINISH":

                textFinish_HP.text = "Lives left: " + PlayerData.HP;
                textFinish_Score.text = textScore.text;
                textFinish_Time.text = textTime.text;
                PlayerData.SCORE += 10 * PlayerData.HP+ PlayerData.TIME;

                panel_gameFinish.SetActive(true);
                break;

            case "GAME_OVER_LIVES":

                textGameOver_Cause.text = "OUT OF LIVES!";

                panel_gameOver.SetActive(true);
                break;

            case "GAME_OVER_TIME":

                textGameOver_Cause.text = "OUT OF TIME!";

                panel_gameOver.SetActive(true);
                break;
        }
        textEnd_TotalScore.text = "TOTAL SCORE: " + PlayerData.SCORE;
    }
}
