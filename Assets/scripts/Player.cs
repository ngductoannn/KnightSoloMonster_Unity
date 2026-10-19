using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isGround;
    Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void MoveLeft()
    {
        transform.position += new Vector3(speed*Time.deltaTime*-1, 0, 0);
        transform.localScale = new Vector3(-Math.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
    }
    
    private void MoveRight()
    {
        transform.position += new Vector3(speed*Time.deltaTime*+1, 0, 0);
        transform.localScale = new Vector3(Math.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
    }

    private void Jump()
    {
        rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
        isGround = false;
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