using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UIController;
using System.Linq;
using System.IO;

public class ButtonHelper : MonoBehaviour
{
    private Button buttonThis;
    private void Start()
    {
        buttonThis = GetComponent<Button>();
        buttonThis.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        switch (gameObject.name)
        {
            // MAIN MENU
            case "BUTTON_SETTINGS":
                UIController.Instance.ShowPanelMainMenu("SETTINGS");
                break;
            case "BUTTON_BACKTOMENU":
                UIController.Instance.ShowPanelMainMenu("MAIN_MENU");
                break;

            case "BUTTON_RESET_HISCORE":
                UIController.Instance.ShowPanelMainMenu("HISCORE_CONFIRM");
                break;
            case "BUTTON_HISCORE_YES":
                UIController.Instance.ShowPanelMainMenu("MAIN_MENU");

                PlayerData.HISCORE_NAMES = new string[0];
                PlayerData.HISCORE_SCORES = new int[0];

                HiScoreJSONClass hJsonClass = new HiScoreJSONClass();

                hJsonClass.Names = new string[0];
                hJsonClass.Scores = new int[0];

                string json = JsonUtility.ToJson(hJsonClass);
                File.WriteAllText(Application.dataPath + "/Resources/HiScoreList.json", json);
                break;

            case "BUTTON_HISCORES":
                UIController.Instance.ShowPanelMainMenu("HISCORES");
                break;

            // GAME SCENE
            case "BUTTON_START_SP":
                PlayerData.LEVEL = 1;
                SceneManager.LoadScene("S02_GAME_SP");
                break;
            case "BUTTON_EXIT_GAME":
                Application.Quit();
                break;

            case "BUTTON_RETURN_MENU":
                SceneManager.LoadScene("S01_MAINMENU");
                break;

            case "BUTTON_ADD_HISCORE":
                UIController.Instance.ShowPanel("GAME_ADD_HISCORE");
                break;
            case "BUTTON_VIEW_HISCORE":
                UIController.Instance.ShowPanel("GAME_HISCORE");
                break;
            case "BUTTON_RETURN_GAME_END":
                UIController.Instance.ShowPanel("GAME_FINISH_RE");
                break;

            case "BUTTON_NEXT_LEVEL":
                PlayerData.LEVEL += 1;
                LevelController.Instance.LevelStart();
                UIController.Instance.HidePanel();
                break;

            //DEBUG
            case "BUTTON_DEBUG":
                UIController.Instance.DebugGame();
                break;
        }
    }
}
