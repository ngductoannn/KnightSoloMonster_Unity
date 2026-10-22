using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float speedMove;
    [SerializeField] private float jumpForce;
    [SerializeField] private float horizontal;

    [Header("Check Movement")]
    [SerializeField] private bool isGround;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canJump = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleDeclaration();
        HandleInput();
        HandleMovement();
        HandleAnimation();
    }

    private void HandleDeclaration()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && canJump)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }
    }

    private void HandleMovement()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontal*speedMove, rb.velocity.y);
            if(rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(Math.Abs(transform.localScale.x), transform.localScale.y,transform.localScale.z);    
            }
            else if(rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y,transform.localScale.z);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }

    private void HandleAnimation()
    {
        if (isGround)
        {
            if(rb.velocity.x != 0)
            {
                animator.SetInteger("State", 1);
            }   
            else if(rb.velocity.x == 0)
            {
                animator.SetInteger("State", 0);
            }
        }
        else
        {
            animator.SetInteger("State", 2);
            animator.SetFloat("Yvelocity", rb.velocity.y);
        }
    }

    public void CanMoveAndJump()
    {
        canMove = true;
        canJump = true;
    }

    public void NotCanMoveAndJump()
    {
        canMove = false;
        canJump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGround = false;
        }
    }

}
