using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform transPlayer;
    private Rigidbody2D rigidPlayer;
    private Animator animPlayer;
    private SpriteRenderer spritePlayer;

    [SerializeField] private bool playerStateOnAir = false, playerStateHurt = false, playerCanMove = true;

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

    private void Update()
    {
        if (playerCanMove)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidPlayer.velocity = new Vector2(-3, rigidPlayer.velocity.y);
                AnimationSetWalk(true, "left");
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rigidPlayer.velocity = new Vector2(3, rigidPlayer.velocity.y);
                AnimationSetWalk(true, "right");
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                rigidPlayer.velocity = new Vector2(0, rigidPlayer.velocity.y);
                AnimationSetWalk(false);
            }

            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !playerStateOnAir)
            {
                //playerStateOnAir = true;
                rigidPlayer.velocity += new Vector2(0, 8.5f);
                AudioController.Instance.PlayAudio(AudioController.AudioType.Jump);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SCENE_GROUND")
        {
            if (playerStateOnAir && rigidPlayer.velocity.y == 0)
                playerStateOnAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "SCENE_GROUND")
        {
            playerStateOnAir = true;
        }
    }

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

    public void TogglePlayerMovement(bool playerCanMoveCond = false)
    {
        rigidPlayer.velocity = new Vector2(0, 0);
        AnimationSetWalk(false);

        playerCanMove = playerCanMoveCond;
    }

    public enum DamageDirection { HitLeft, HitRight, Fell };
    public void PlayerHurt(DamageDirection dmgDir, Transform respawnPt = null)
    {
        if (!playerStateHurt)
        {
            PlayerData.P1_HP -= 1;
            UIController.Instance.UpdateUI();
            AudioController.Instance.PlayAudio(AudioController.AudioType.Hit);

            playerStateHurt = true;
            playerCanMove = false;

            //int force = 150;
            float velocity = 0.2f;
            switch (dmgDir)
            {
                case DamageDirection.HitLeft:
                    rigidPlayer.velocity = new Vector2(-velocity, 4);
                    //rigidPlayer.AddForce(new Vector2(-force, 300));
                    break;
                case DamageDirection.HitRight:
                    rigidPlayer.velocity = new Vector2(velocity, 4);
                    //rigidPlayer.AddForce(new Vector2(force, 300));
                    break;
                case DamageDirection.Fell:
                    if (respawnPt != null)
                    {
                        transform.position = respawnPt.position;
                        TogglePlayerMovement();
                    }
                    break;
            }

            StartCoroutine(coroutineInvincible());
        }
        else
        {
            switch (dmgDir)
            {
                case DamageDirection.Fell:
                    if (respawnPt != null)
                    {
                        transform.position = respawnPt.position;
                        TogglePlayerMovement();
                    }
                    break;
            }
        }
    }
    public void PlayerJump(float velocity)
    {
        //8.5f is defualt jump

        rigidPlayer.velocity += new Vector2(0, velocity);
    }
    public void ResetPosition()
    {
        transform.localPosition = PlayerData.P1_SPAWN;
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
