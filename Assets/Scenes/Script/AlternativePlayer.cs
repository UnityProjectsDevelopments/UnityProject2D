using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativePlayer : MonoBehaviour
{
    public float speed;

    public float jumpForce;

    public bool isGrounded;// Check if he's on the ground

    public Rigidbody2D rb;// Physics

    private float moveInput;


    // Start is called before the first frame update
    void Start()
    {
        // Just make sure he will take this component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

            isGrounded = false;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 7;
        }
        else
        {
            speed = 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = true;
            
        }
    }
}
