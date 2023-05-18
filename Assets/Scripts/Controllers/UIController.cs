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

    public void UpdateUI()
    {
        textHP.text = "HP: x" + PlayerStatController.HP;
        textScore.text = "Score: " + PlayerStatController.SCORE;
    }
}
