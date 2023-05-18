using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

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

    [Header("MAIN MENU")]
    [SerializeField] private GameObject panel_mainMenu;
    [SerializeField] private GameObject panel_settings;
    [SerializeField] private GameObject panel_settings_confirm_hiscore;
    [SerializeField] private GameObject panel_hiscore_mainmenu;
    [SerializeField] private TextMeshProUGUI text_HiScore_Names_mainmenu;
    [SerializeField] private TextMeshProUGUI text_HiScore_Scores_mainmenu;

    [Space(10)]

    [Header("GAME SCENE")]
    [SerializeField] private TextMeshProUGUI textHP;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private GameObject panel_gameEnd;
    [SerializeField] private TextMeshProUGUI textEnd_Title;
    [SerializeField] private TextMeshProUGUI textEnd_TotalScore;
    [SerializeField] private TextMeshProUGUI textEnd_Name;
    [SerializeField] private Button button_continue;
    [SerializeField] private GameObject panel_gameFinish;
    [SerializeField] private TextMeshProUGUI textFinish_HP;
    [SerializeField] private TextMeshProUGUI textFinish_Score;
    [SerializeField] private TextMeshProUGUI textFinish_Time;
    [SerializeField] private GameObject panel_gameOver;
    [SerializeField] private TextMeshProUGUI textGameOver_Cause;
    [SerializeField] private GameObject panel_hiscore;
    [SerializeField] private TextMeshProUGUI textHiScore_Names;
    [SerializeField] private TextMeshProUGUI textHiScore_Scores;

    public class HiScoreJSONClass
    {
        public string[] Names;
        public int[] Scores;
    }

    private void Start()
    {
        if (PlayerData.HISCORE_NAMES.Length <= 0)
        {
            TextAsset jsonText = Resources.Load<TextAsset>("JSON/HiScoreList") as TextAsset;
            string strJson = jsonText.text;
            HiScoreJSONClass dJsonClass = JsonUtility.FromJson<HiScoreJSONClass>(strJson);

            PlayerData.HISCORE_NAMES = dJsonClass.Names;
            PlayerData.HISCORE_SCORES = dJsonClass.Scores;
        }

        if (textHP != null)
            UpdateUI();
    }

    public void UpdateUI()
    {
        textHP.text = "P1 HP: x" + PlayerData.P1_HP;
        textScore.text = "P1 Score: " + PlayerData.P1_SCORE;
    }
    public void UpdateHiScore(bool isMainMenu = false)
    {
        string text_scores = "", text_names = "";
        for (int i = 0; i < PlayerData.HISCORE_NAMES.Length; i++)
        {
            text_scores += PlayerData.HISCORE_SCORES[i] + "\n";
            text_names += (i + 1) + ". " + PlayerData.HISCORE_NAMES[i] + "\n";
        }

        if (!isMainMenu)
        {
            textHiScore_Names.text = text_names;
            textHiScore_Scores.text = text_scores;
        }
        else
        {
            text_HiScore_Names_mainmenu.text = text_names;
            text_HiScore_Scores_mainmenu.text = text_scores;
        }
    }

    public void UpdateTimeUI()
    {
        textTime.text = "Time: " + PlayerData.TIME;
    }

    public void ShowPanel(string panel_type)
    {
        LevelController.Instance.LevelEnd();

        panel_gameEnd.gameObject.SetActive(false);
        panel_gameFinish.gameObject.SetActive(false);
        panel_gameOver.gameObject.SetActive(false);
        panel_hiscore.gameObject.SetActive(false);

        switch (panel_type)
        {
            case "GAME_FINISH":

                textEnd_Title.text = "LEVEL COMPLETE!";
                textFinish_HP.text = "Lives left: " + PlayerData.P1_HP;
                textFinish_Score.text = textScore.text;
                textFinish_Time.text = textTime.text;
                PlayerData.P1_SCORE += 10 * PlayerData.P1_HP+ PlayerData.TIME;

                panel_gameEnd.gameObject.SetActive(true);
                panel_gameFinish.SetActive(true);
                break;

            case "GAME_OVER_LIVES":

                textEnd_Title.text = "GAME OVER!";
                textGameOver_Cause.text = "OUT OF LIVES!";

                panel_gameEnd.gameObject.SetActive(true);
                panel_gameOver.SetActive(true);
                break;

            case "GAME_OVER_TIME":

                textEnd_Title.text = "GAME OVER!";
                textGameOver_Cause.text = "OUT OF TIME!";

                panel_gameEnd.gameObject.SetActive(true);
                panel_gameOver.SetActive(true);
                break;

            case "GAME_HISCORE":

                LevelController.Instance.AddHiScore(textEnd_Name.text, PlayerData.P1_SCORE);
                UpdateHiScore();
                panel_gameEnd.gameObject.SetActive(true);
                panel_hiscore.SetActive(true);
                break;
        }
        textEnd_TotalScore.text = "TOTAL SCORE: " + PlayerData.P1_SCORE;


    }

    public void ShowPanelMainMenu(string panel_type)
    {
        panel_mainMenu.gameObject.SetActive(false);
        panel_settings.gameObject.SetActive(false);
        panel_settings_confirm_hiscore.gameObject.SetActive(false);
        panel_hiscore_mainmenu.gameObject.SetActive(false);

        switch (panel_type)
        {
            case "MAIN_MENU":

                panel_mainMenu.gameObject.SetActive(true);
                break;

            case "SETTINGS":

                panel_settings.gameObject.SetActive(true);
                break;

            case "HISCORE_CONFIRM":

                panel_settings_confirm_hiscore.gameObject.SetActive(true);
                break;

            case "HISCORES":

                UpdateHiScore(true);
                panel_hiscore_mainmenu.gameObject.SetActive(true);
                break;
        }
    }
}
