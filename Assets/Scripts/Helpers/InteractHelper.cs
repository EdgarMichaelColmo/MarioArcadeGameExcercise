using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHelper : MonoBehaviour
{
    [SerializeField] private string Interact_ID;
    [SerializeField] private int value;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (Interact_ID)
        {
            case "INTERACT_COIN":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    //print("+1 coin");
                    PlayerStatController.SCORE += value;
                    UIController.Instance.UpdateUI();
                    gameObject.SetActive(false);
                }
                break;
            case "INTERACT_SPIKE":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    //print("+1 coin");
                    PlayerStatController.HP -= 1;
                    UIController.Instance.UpdateUI();
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
