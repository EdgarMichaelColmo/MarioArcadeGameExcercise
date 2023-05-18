using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Coroutine coroutineTime;

    private void Start()
    {
        PlayerData.SCORE = 0;
        PlayerData.HP = 3;
        PlayerData.TIME = LevelTime;

        coroutineTime = StartCoroutine(c_timer());
    }

    public void LevelEnd()
    {
        PlayerController.Instance.playerCanMove = false;
        PlayerController.Instance.AnimationSetWalk(false);
        StopCoroutine(coroutineTime);
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
