using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class InteractHelper : MonoBehaviour
{
    [SerializeField] private string Interact_ID;
    [SerializeField] private int value;
    [SerializeField] private GameObject gameobj;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (Interact_ID)
        {
            case "INTERACT_SPIKE":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    if (value == 0)
                    {
                        collision.gameObject.GetComponent<PlayerController>().PlayerHurt(true);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<PlayerController>().PlayerHurt(false);
                    }
                }
                break;
            case "INTERACT_SLIME_WEAKNESS":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    PlayerData.P1_SCORE += value;
                    UIController.Instance.UpdateUI();
                    gameobj.GetComponent<EnemyController>().enemyCanDamage = false;
                    gameobj.GetComponent<EnemyController>().EnemyHurt();

                    GetComponent<Collider2D>().enabled = false;
                    enabled = false;
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (Interact_ID)
        {
            case "INTERACT_COIN":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    PlayerData.P1_SCORE += value;
                    UIController.Instance.UpdateUI();
                    gameObject.SetActive(false);
                }
                break;
            case "INTERACT_FLAG":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    UIController.Instance.ShowPanel("GAME_FINISH");
                }
                break;
        }
    }
}
