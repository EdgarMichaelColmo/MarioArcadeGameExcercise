using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UIController;
using System.IO;

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

    [SerializeField] private int LevelTime;
    [SerializeField] private Transform enemiesList;

    private Coroutine coroutineTime;

    private void Start()
    {
        PlayerData.P1_SCORE = 0;
        PlayerData.P1_HP = 3;
        PlayerData.TIME = LevelTime;

        coroutineTime = StartCoroutine(c_timer());
    }

    public void LevelEnd()
    {
        foreach (var i in enemiesList.GetComponentsInChildren<EnemyController>())
        {
            i.enemyCanDamage = false;
            i.alive = false;
        }

        PlayerController.Instance.playerCanMove = false;
        PlayerController.Instance.AnimationSetWalk(false);
        StopCoroutine(coroutineTime);
    }

    public void AddHiScore(string name, int score)
    {
        TextAsset jsonText = Resources.Load<TextAsset>("JSON/HiScoreList") as TextAsset;
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
        File.WriteAllText(Application.dataPath + "/Resources/JSON/HiScoreList.json", json);

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
