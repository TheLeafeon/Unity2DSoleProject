using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMonster : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    

    public float monsterSight = 0.0f;
    public float monsterSpeed = 0.0f;
    bool isMonsterActive = false;
    public float monsterLockDirection = 0.0f;

    Vector2 lookDirection = new Vector2(1, 0);

    Animator animator;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lookDirection.x = monsterLockDirection;
    }

    // Update is called once per frame
    void Update()
    {



        if (!isMonsterActive)
        {
            UnityEngine.Debug.DrawRay(rigidbody2d.position, Vector3.left* monsterSight, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2d.position, Vector3.left, monsterSight, LayerMask.GetMask("Player"));

            if(rayHit.collider != null)
            {
                UnityEngine.Debug.Log(rayHit.collider.name);
                isMonsterActive = true;
            }
        }

        if(isMonsterActive)
        {
            Vector2 move = new Vector2(lookDirection.x, rigidbody2d.velocity.y);

            rigidbody2d.velocity = new Vector2(move.x * monsterSpeed, rigidbody2d.velocity.y);

            RaycastHit2D wallHit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 1.0f, LayerMask.GetMask("Platform"));
            if (wallHit.collider != null)
            {
                lookDirection.x *= -1;

                spriteRenderer.flipX = lookDirection.x == 1.0f;
            }

        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        //플레이어 충돌
        PlayerController p = other.collider.GetComponent<PlayerController>();

        if (p != null && isMonsterActive)
        {
            UnityEngine.Debug.Log("MushroomMonster_Head Collision with " + other.gameObject);

            isMonsterActive = false;

            p.LaunchCharacter(10.0f);

            animator.SetBool("isAlive", false);

            
        }
    }


}
