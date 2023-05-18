using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform transPlayer;
    private Rigidbody2D rigidPlayer;
    private Animator animPlayer;
    private SpriteRenderer spritePlayer;

    private bool playerStateOnAir = false, playerStateHurt = false;
    public bool playerCanMove = true;

    public static PlayerController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        transPlayer = transform;
        rigidPlayer = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (playerCanMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transPlayer.localPosition += new Vector3(-0.1f, 0, 0);
                AnimationSetWalk(true, "left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transPlayer.localPosition += new Vector3(0.1f, 0, 0);
                AnimationSetWalk(true, "right");
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                AnimationSetWalk(false);
            }

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !playerStateOnAir)
            {
                playerStateOnAir = true;
                rigidPlayer.AddForce(new Vector2(0, 500));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SCENE_GROUND")
        {
            playerStateOnAir = false;
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "SCENE_GROUND")
    //    {
    //        playerStateOnAir = true;
    //    }
    //}

    public void AnimationSetWalk(bool cond, string flip = "")
    {
        if (animPlayer.GetBool("isWalking") != cond)
        {
            animPlayer.SetBool("isWalking", cond);
        }

        if (flip != "")
        {
            if (flip == "left" && !spritePlayer.flipX)
                spritePlayer.flipX = true;
            else if (flip == "right" && spritePlayer.flipX)
                spritePlayer.flipX = false;
        }
    }

    public void PlayerHurt(bool directionIsLeft)
    {
        if (!playerStateHurt)
        {
            playerStateHurt = true;
            playerCanMove = false;

            PlayerData.P1_HP -= 1;
            UIController.Instance.UpdateUI();

            int force = 150;
            if (directionIsLeft)
            {
                rigidPlayer.AddForce(new Vector2(-force, 300));
            }
            else
            {
                rigidPlayer.AddForce(new Vector2(force, 300));
            }

            StartCoroutine(coroutineInvincible());
        }
    }

    private IEnumerator coroutineInvincible()
    {
        float time = 2, loop = 16;

        for (int i = 0; i < loop; i++)
        {
            if (i % 2 == 0)
                spritePlayer.color = Color.red;
            else
                spritePlayer.color = Color.white;
            yield return new WaitForSeconds(time / loop);
        }


        playerStateHurt = false;
        playerCanMove = true;

        if (PlayerData.P1_HP <= 0)
        {
            UIController.Instance.ShowPanel("GAME_OVER_LIVES");
        }
    }
}
