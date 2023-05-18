using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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
                    PlayerData.SCORE += value;
                    UIController.Instance.UpdateUI();
                    gameObject.SetActive(false);
                }
                break;
            case "INTERACT_SPIKE":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    if (value == 0)
                    {
                        PlayerController.Instance.PlayerHurt(true);
                    }
                    else
                    {
                        PlayerController.Instance.PlayerHurt(false);
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (Interact_ID)
        {
            case "INTERACT_FLAG":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    UIController.Instance.ShowPanel("GAME_FINISH");
                }
                break;
        }
    }
}
