using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private string enemyType;

    public bool alive = true;
    private Rigidbody2D rigidbody_;
    private Animator animator;
    private SpriteRenderer sprite;

    public bool enemyCanDamage = true;

    private void Start()
    {
        rigidbody_ = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(c_RandomEnemyState());
    }

    private IEnumerator c_RandomEnemyState()
    {
        float stateDurationRangeMin = 2f;
        float stateDurationRangeMax = 4f;

        while (alive)
        {
            float randomState = Random.Range(0, 2);

            EnemyState((State)randomState);

            if (randomState == 1)
            {
                animator.SetBool("move", true);
                yield return new WaitForSeconds(0.1f);
                animator.SetBool("move", false);
            }

            float RandomDuration = Random.Range(stateDurationRangeMin, stateDurationRangeMax);
            yield return new WaitForSeconds(RandomDuration);

        }
    }

    private enum State { Idle, Move, Attack };
    private void EnemyState(State currentState)
    {
        //print("Enemy " + currentState);
        switch (currentState)
        {
            case State.Move:

                //float rangeX = Random.Range(-300, 300f), rangeY = Random.Range(250, 400);
                float rangeX = Random.Range(-20, 20f), rangeY = Random.Range(3, 10);

                if (rangeX > 0)
                    sprite.flipX = false;
                else
                    sprite.flipX = true;

                rigidbody_.velocity = (new Vector2(rangeX, rangeY));
                break;
            case State.Attack:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && enemyCanDamage)
        {
            Vector2 vect_direction = collision.transform.localPosition - transform.localPosition;

            PlayerController.DamageDirection dmgDir = PlayerController.DamageDirection.HitRight;
            if (vect_direction.x < 0)
                dmgDir = PlayerController.DamageDirection.HitLeft;

            collision.gameObject.GetComponent<PlayerController>().PlayerHurt(dmgDir);
        }
    }

    public void EnemyHurt()
    {
        alive = false;
        transform.parent.gameObject.SetActive(false);
    }
}
