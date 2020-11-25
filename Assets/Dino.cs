using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rgb2D;
    Animator animator;
    bool isDead;
    public float jumpforce;
    bool m_isGround;

    public bool IsGround { get => m_isGround; }

    private void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead || !GameManager.Ins.IsGameBegun) return;

        Flip();
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11f, 11f), transform.position.y, transform.position.z);


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rgb2D.AddForce(Vector2.up * jumpforce);

        //}

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rgb2D.velocity.y) < 0.1)
        {
            rgb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);

        }
        if (Input.GetButtonDown("Down"))
        {
            if (animator)
            {
                animator.SetBool("Entry", true);
            }

        }
        // rgb2D.AddForce(new Vector2(Vector2.up * jumpforce), ForceMode2D.Impulse);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    animator.SetBool("Grounded", grounded);
        //    if (grounded)
        //    {
        //        grounded = false;
        //        rigidbody2D.AddForce(Vector2.up * jumpPow);
        //    }
        //}
    }

    private void FixedUpdate()
    {
       if (isDead || !GameManager.Ins.IsGameBegun) return;

        MoveHandle();
    }

    void MoveHandle()
    {

        if (GamePadsController.Ins.CanMoveLeft)
        {
            if (rgb2D)
            {
                rgb2D.velocity = new Vector2(-1f, rgb2D.velocity.y) * moveSpeed;
            }

            if (animator)
            {
                animator.SetBool("Run", true);
            }
        }
        else if (GamePadsController.Ins.CanMoveRight)
        {
            if (rgb2D)
            {
                rgb2D.velocity = new Vector2(1f, rgb2D.velocity.y) * moveSpeed;
            }

            if (animator)
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            if (rgb2D)
            {
                rgb2D.velocity = new Vector2(0, rgb2D.velocity.y);
            }

            if (animator)
            {
                animator.SetBool("Run", false);
            }
           }


        }
    void Flip()
    {
        if(GamePadsController.Ins.CanMoveLeft)
        {
            if(transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }else if(GamePadsController.Ins.CanMoveRight)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void Die()
    {
        isDead = true;
        if(animator)
        {
            animator.SetTrigger("Dead");
        }
        if(rgb2D)
        {
            rgb2D.velocity = new Vector2(0f, rgb2D.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            if (isDead) return;


            Stone stone = collision.gameObject.GetComponent<Stone>();

            if (stone && !stone.IsGround)
            {
                Die();

                GameManager.Ins.IsGameOver = true; 
            }
        }


        if (Input.GetButtonDown("Jump") && Mathf.Abs(rgb2D.velocity.y) < 0.1)
        {
            Dino dino = collision.gameObject.GetComponent<Dino>();
      
            if (dino.m_isGround)
            {
                dino.m_isGround = false;
                rgb2D.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }
        }

    }

}
