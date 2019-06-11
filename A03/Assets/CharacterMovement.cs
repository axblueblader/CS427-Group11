using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 20F;
    public float jumpSpeed = 200F;
    public float maxVelocity = 2.3F;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, jumpSpeed));
            }

            SetCharState();
            if (Math.Abs(rb.velocity.x) > maxVelocity)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
            }
        }

        
    }

    void SetCharState()
    {
        //Debug.Log(rb.velocity.x);
        if (Math.Abs(rb.velocity.x) >= 0.3F)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "FatalObj")
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
