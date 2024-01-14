using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    Animator animator;

    //Vector2 lookDirection = new Vector2(1, 0);

    //캐릭터 스테이터스
    public float playerSpeed = 0.0f;
    public float playerJumpPower = 0.0f;
    private bool isFly = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //방향전환
        if(Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //점프
        if (Input.GetButtonDown("Jump") && !isFly)
        {
            isFly = true;
            rigidbody2d.AddForce(Vector2.up * playerJumpPower, ForceMode2D.Impulse);
            
            UnityEngine.Debug.Log("Jump");
        }

    }

    void FixedUpdate()
    {
        //float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
       

        //Vector2 position = rigidbody2d.position;

        Vector2 move = new Vector2(horizontal, rigidbody2d.velocity.y);

        if (!Mathf.Approximately(move.x, 0.0f))
        {
            //lookDirection.x = move.x;
            
            //lookDirection.Normalize();
        }
        if(Mathf.Abs(move.x)>0.3f)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        rigidbody2d.velocity = new Vector2(move.x * playerSpeed, rigidbody2d.velocity.y);


        //점프 감지
        if(rigidbody2d.velocity.y < 0.0f)
        {
            UnityEngine.Debug.DrawRay(rigidbody2d.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2d.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.6f)
                {
                    UnityEngine.Debug.Log(rayHit.collider.name);
                    isFly = false;
                }
            }
        }
            
        
    }
}
