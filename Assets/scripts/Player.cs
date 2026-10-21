using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Check")]
    [SerializeField] private bool isGround;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canJump = true;

    private Animator animator;

    private float horizontal;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }   

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleAnimation();
    }

    public void EnableMovementAndJump()
    {
        canMove = true;
        canJump = true;
    }

    public void DisableMovementAndJump()
    {
        canJump = false;
        canMove = false;
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    private void HandleMovement()
    {
        if (canMove)
        {
            if(horizontal > 0)
            {
                rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
                transform.localScale = new Vector2(Math.Abs(transform.localScale.x), transform.localScale.y);

            }
            else if(horizontal < 0)
            {
                rb.velocity = new Vector2(horizontal*speed, rb.velocity.y);
                transform.localScale = new Vector2(-Math.Abs(transform.localScale.x), transform.localScale.y);
            }   
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

    }

    private void HandleAnimation()
    {
        animator.SetBool("isGround", isGround);
        animator.SetFloat("Xvelocity", rb.velocity.x);
        animator.SetFloat("Yvelocity", rb.velocity.y);
    }

    private void Jump()
    {
        if (isGround && canJump)
        {
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            isGround = false;
        }
    }

    private void Attack()
    {
        if(isGround == true)
        {
            animator.SetTrigger("attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }


}

// code so clean