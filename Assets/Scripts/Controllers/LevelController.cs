using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UIController;
using System.IO;
using UnityEngine.Jobs;
using System.Reflection;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    [SerializeField] private int levelTime;
    private LevelHelper[] levelList;
    public static int LEVEL_COUNT = 0;

    private Coroutine coroutineTime;

    private void Start()
    {
        levelList = transform.GetComponentsInChildren<LevelHelper>();
        LEVEL_COUNT = levelList.Length;
        LevelStart();

        PlayerData.P1_SCORE = 0;
        PlayerData.P1_HP = 3;
    }

    public void LevelStart()
    {
        coroutineTime = StartCoroutine(c_timer());
        PlayerController.Instance.TogglePlayerMovement(true);
        UIController.Instance.UpdateUI();
        AudioController.Instance.PlayBGM(PlayerData.LEVEL - 1);

        foreach (var lvl in levelList)
        {
            lvl.transform.GetChild(0).gameObject.SetActive(false);
        }


        if (PlayerData.LEVEL - 1 < levelList.Length)
        {
            PlayerData.LEVEL_FINISH = false;
            PlayerData.TIME = levelTime;
            PlayerController.Instance.ResetPosition();

            levelList[PlayerData.LEVEL - 1].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void LevelEnd()
    {
        foreach (var i in levelList[PlayerData.LEVEL - 1].EnemyList.GetComponentsInChildren<EnemyController>())
        {
            i.enemyCanDamage = false;
            i.alive = false;
        }

        PlayerController.Instance.TogglePlayerMovement();
        StopCoroutine(coroutineTime);
    }

    public bool CheckIfNextLevelAvailable()
    {
        return PlayerData.LEVEL < levelList.Length;
    }

    public void AddHiScore(string name, int score)
    {
        TextAsset jsonText = Resources.Load<TextAsset>("HiScoreList") as TextAsset;
        string strJson = jsonText.text;
        HiScoreJSONClass hJsonClass = JsonUtility.FromJson<HiScoreJSONClass>(strJson);

        List<string> list_names = PlayerData.HISCORE_NAMES.ToList();
        List<int> list_scores = PlayerData.HISCORE_SCORES.ToList();

        list_names.Add(name);
        list_scores.Add(score);

        #region SORTING LOGIC
        int x = 0;
        while (x < list_scores.Count)
        {
            for (int i = x + 1; i < list_scores.Count; i++)
            {
                if (list_scores[x] < list_scores[i])
                {
                    (list_scores[i], list_scores[x]) = (list_scores[x], list_scores[i]);
                    (list_names[i], list_names[x]) = (list_names[x], list_names[i]);

                    i = x + 1;
                }
            }

            x++;
        }
        #endregion

        if (list_names.Count > 10)
        {
            list_scores.RemoveAt(list_names.Count - 1);
            list_names.RemoveAt(list_names.Count - 1);
        }

        hJsonClass.Names = list_names.ToArray();
        hJsonClass.Scores = list_scores.ToArray();


        string json = JsonUtility.ToJson(hJsonClass);
        File.WriteAllText(Application.dataPath + "/Resources/HiScoreList.json", json);

        PlayerData.HISCORE_NAMES = list_names.ToArray();
        PlayerData.HISCORE_SCORES = list_scores.ToArray();
    }

    private IEnumerator c_timer()
    {
        while(PlayerData.TIME > 0)
        {
            PlayerData.TIME -= 1;
            UIController.Instance.UpdateTimeUI();
            yield return new WaitForSeconds(1);
        }

        if(PlayerData.TIME == 0)
        {
            UIController.Instance.ShowPanel("GAME_OVER_TIME");
        }
    }
}
