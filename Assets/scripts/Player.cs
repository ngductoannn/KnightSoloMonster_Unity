using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Declaration")]
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D hitBox;

    [Header("Movement")]
    [SerializeField] private float speedMove;
    [SerializeField] private float jumpForce;
    [SerializeField] private float horizontal;

    [Header("Check Jump")]
    [SerializeField] private bool isGround;

    [Header("Check Slice")]
    [SerializeField] private float timeSpawnSlice = 0f;
    [SerializeField] private Vector2 hitBoxOffsetStand;
    [SerializeField] private Vector2 hitBoxSizeStand;
    [SerializeField] private Vector2 hitBoxOffsetSlice;
    [SerializeField] private Vector2 hitBoxSizeSlice;

    [Header("Check Attack")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private float timeSpawnAttack = 0f;

    [Header("Attack Damage")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damage = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        hitBox = GetComponentInChildren<CapsuleCollider2D>();

        hitBoxOffsetStand = hitBox.offset;
        hitBoxSizeStand = hitBox.size;
        hitBoxOffsetSlice = new Vector2(hitBox.offset.x, -0.3f);
        hitBoxSizeSlice = new Vector2(hitBox.size.x, 1.1f);
    }

    void Update()
    {
        HandleTime();
        HandleDeclaration();
        HandleInput();
        HandleMovement();
        HandleAnimation();
    }

    private void HandleTime()
    {
        timeSpawnAttack -= Time.deltaTime;
        timeSpawnSlice -= Time.deltaTime;
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
        else if (Input.GetKeyDown(KeyCode.J) && isGround && timeSpawnAttack <= 0)
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.K) && isGround && timeSpawnSlice <= 0)
        {
            Slice();
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
        timeSpawnAttack = 0.5f;
    }

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(damage);
        }
    }
    private void Slice()
    {
        animator.SetTrigger("Slice");
        timeSpawnSlice = 0.5f;
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

    public void HitBoxBeforeAndAfterSlice()
    {
        hitBox.size = hitBoxSizeStand;
        hitBox.offset = hitBoxOffsetStand;
    }

    public void HitBoxWhenSlice()
    {
        hitBox.offset = hitBoxOffsetSlice;
        hitBox.size = hitBoxSizeSlice;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
