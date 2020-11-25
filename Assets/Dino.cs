using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rgb2D;
    Animator animator;
    bool isDead;

    private void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
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

    }

}
