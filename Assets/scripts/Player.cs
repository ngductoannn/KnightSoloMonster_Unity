using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool isGround;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(speed*Time.deltaTime*-1, 0, 0);
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed*Time.deltaTime*+1, 0, 0);
            transform.localScale = new Vector3(1,1,1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            rb.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
            isGround = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
}
