using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform transPlayer;
    private Rigidbody2D rigidPlayer;
    private Animator animPlayer;
    private SpriteRenderer spritePlayer;
    [SerializeField] private bool playerStateOnAir = false;

    void Start()
    {
        transPlayer = transform;
        rigidPlayer = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
        spritePlayer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
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

    private void AnimationSetWalk(bool cond, string flip = "")
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
}
