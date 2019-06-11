using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 30F;
    public float jumpSpeed = 400F;
    public float maxVelocityX = 2.3F;
    public float maxVelocityY = 4F;
    public float minVelocityXAsRun = 0.5F;
    private Animator animator;
    private Rigidbody2D rb;
    private bool disableMovement;
    // Start is called before the first frame update
    void Start()
    {
        disableMovement = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableMovement) {
            //Debug.Log("Movement is disabled");
        } else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector2(-speed, 0));
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector2(+speed, 0));
                GetComponent<SpriteRenderer>().flipX = false;
            }

            // Space pressed and not moving up or down
            if (Input.GetKeyDown(KeyCode.Space) && Math.Abs(rb.velocity.y) == 0)
            {
                rb.AddForce(new Vector2(0, jumpSpeed));
            }

            SetCharState();
            LimitCharVelocity();
        }

        
    }

    void LimitCharVelocity()
    {
        if (Math.Abs(rb.velocity.x) > maxVelocityX)
        {
            rb.velocity = new Vector2(maxVelocityX * Math.Sign(rb.velocity.x), rb.velocity.y);
        }

        if (Math.Abs(rb.velocity.y) > maxVelocityY)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocityY * Math.Sign(rb.velocity.y));
        }
    }
    void SetCharState()
    {
        //Debug.Log(rb.velocity.y);
        if (Math.Abs(rb.velocity.x) >= minVelocityXAsRun)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "FatalObj" && collision.otherCollider.gameObject.tag == "Player")
        {
            animator.SetTrigger("Dying");
            disableMovement = true;
        }
    }

    public void onDyingAnimEnded()
    {
        animator.SetBool("isDead", true);
    }
}
