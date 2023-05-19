using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class InteractHelper : MonoBehaviour
{
    [SerializeField] private string Interact_ID;
    [SerializeField] private int value1, value2;
    [SerializeField] private GameObject gameobj;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (Interact_ID)
        {
            case "INTERACT_SPIKE":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    if (value1 == 0)
                    {
                        collision.gameObject.GetComponent<PlayerController>().PlayerHurt(PlayerController.DamageDirection.HitLeft);
                    }
                    else
                    {
                        collision.gameObject.GetComponent<PlayerController>().PlayerHurt(PlayerController.DamageDirection.HitRight);
                    }
                }
                break;
            case "INTERACT_SLIME_WEAKNESS":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    PlayerData.P1_SCORE += value1;
                    UIController.Instance.UpdateUI();
                    collision.gameObject.GetComponent<PlayerController>().PlayerJump(value2);
                    gameobj.GetComponent<EnemyController>().enemyCanDamage = false;
                    gameobj.GetComponent<EnemyController>().EnemyHurt();

                    GetComponent<Collider2D>().enabled = false;
                    enabled = false;
                }
                break;
            case "INTERACT_FELL":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    collision.gameObject.GetComponent<PlayerController>().PlayerHurt(PlayerController.DamageDirection.Fell, gameobj.transform);
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
                    AudioController.Instance.PlayAudio(AudioController.AudioType.PickUp);
                    PlayerData.P1_SCORE += value1;
                    UIController.Instance.UpdateUI();
                    gameObject.SetActive(false);
                }
                break;
            case "INTERACT_FLAG":
                if (collision.gameObject.tag.Equals("Player"))
                {
                    AudioController.Instance.PlayAudio(AudioController.AudioType.LevelComplete);
                    PlayerData.LEVEL_FINISH = true;
                    UIController.Instance.ShowPanel("GAME_FINISH");
                }
                break;
        }
    }
}
