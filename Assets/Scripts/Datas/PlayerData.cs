using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UIController;

public class PlayerData : MonoBehaviour
{
    public static int LEVEL = 1;
    public static bool LEVEL_FINISH = false;
    public static int TIME = 100;

    public static Vector3 P1_SPAWN = new Vector3(0, 0.19f, 0);
    public static int P1_SCORE = 0;
    public static int P1_HP = 3;

    public static string[] HISCORE_NAMES = new string[0];
    public static int[] HISCORE_SCORES = new int[0];
}
