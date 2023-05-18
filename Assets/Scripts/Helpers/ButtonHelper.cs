using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            case "BUTTON_START_SP":
                SceneManager.LoadScene("S02_GAME_SP");
                break;
            case "BUTTON_EXIT_GAME":
                Application.Quit();
                break;

            case "BUTTON_RETURN_MENU":
                SceneManager.LoadScene("S01_MAINMENU");
                break;
        }
    }
}
