using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class JSONEditor : EditorWindow
{

#if UNITY_EDITOR

    [MenuItem("Window/JSON Editor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<JSONEditor>("JSON Editor");
    }


    private UnityEngine.Object selectedJSON;
    private string JSONPath;
    private string cur_Name;
    private int cur_Score;
    private List<string> cur_Names = new List<string>();
    private List<int> cur_Scores = new List<int>();

    private void OnGUI()
    {
        selectedJSON = EditorGUILayout.ObjectField(null, typeof(UnityEngine.Object), true);
        if (selectedJSON != null)
        {
            try
            {
                JSONPath = AssetDatabase.GetAssetPath(selectedJSON);
                JSONPath = JSONPath.Substring(6);
            }
            catch
            {
                EditorGUILayout.LabelField("Error! Wrong File Type");
            }
        }

        cur_Name = EditorGUILayout.TextField("Name", cur_Name);
        cur_Score = EditorGUILayout.IntField("Score", cur_Score);
        if (GUILayout.Button("Add Hi-Score"))
        {
            cur_Names.Add(cur_Name);
            cur_Scores.Add(cur_Score);
            cur_Name = "";
            cur_Score = 0;
        }

        if (GUILayout.Button("Create JSON"))
        {
            CreateJSON();
        }
    }

    private void CreateJSON()
    {
        UIController.HiScoreJSONClass cur_JSON1 = new UIController.HiScoreJSONClass();

        cur_JSON1.Names = cur_Names.ToArray();
        cur_JSON1.Scores = cur_Scores.ToArray();

        string json = JsonUtility.ToJson(cur_JSON1);

        File.WriteAllText(Application.dataPath + "/Resources/JSON/HiScoreList.json", json);

        cur_Names.Clear();
        cur_Scores.Clear();
    }

#endif
}
