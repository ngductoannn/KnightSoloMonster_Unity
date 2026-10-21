using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    [Header("Attack Details")]
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask WhatIsEnemy;

    [Header("Movement Details")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGround;
    [SerializeField] private LayerMask WhatIsGround;

    [Header("Check")]
    [SerializeField] private bool facingRight = true;
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
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimation();
        HandleFlip();
    }

    public void DamageEnemies()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, WhatIsEnemy);

        foreach(Collider2D enemy in enemyColliders){
            enemy.GetComponent<Enemy>().TakeDamage();  
        }
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
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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
        if(isGround)
        {
            animator.SetTrigger("attack");
        }
    }

    private void HandleFlip()
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }

    private void HandleCollision()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, WhatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


}
