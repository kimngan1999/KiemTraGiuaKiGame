using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rgb2D;
    bool m_isGround;

    public bool IsGround { get => m_isGround; }

    private void Awake()
    {
        rgb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rgb2D)
        {
            rgb2D.velocity = Vector3.down * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGround = true;
            Destroy(gameObject, 1f);

            GameManager.Ins.Score++;
        }
    }

 

}
