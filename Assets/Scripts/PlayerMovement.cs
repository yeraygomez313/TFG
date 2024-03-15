using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Horizontal movement velocity
    public float jumpForce = 10f; // Jump force
    public float dashForce = 10f; // Dash force
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDashing = false;
    private float elapsedTime = 0f;
    private float dashingTime = 0.3f;
    private SpriteRenderer playerSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        
        // This has an error, it overrides the force and disables the function of dashing
        if (!isDashing)
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Dash E
        if (Input.GetKeyDown(KeyCode.E) && !isGrounded && elapsedTime > 1f)
        {
            //moveSpeed = dashForce;
            
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(dashForce, 0f);
            isDashing = true;
            elapsedTime = 0f;
        }

        // Dash Q
        if (Input.GetKeyDown(KeyCode.Q) && !isGrounded && elapsedTime > 1f)
        {
            //moveSpeed = dashForce;
            
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(-dashForce, 0f);
            isDashing = true;
            elapsedTime = 0f;
        }

        // It is needed to make a progressive transition, a cooldown and blocking the controls.
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && rb.velocity.x == 0f && elapsedTime > 3f)
        {
            Color newColor = playerSprite.color;
            newColor.a = 0.3f;
            playerSprite.color = newColor;
            elapsedTime = 0f;
        }

        // Changes the opacity to the original opacity
        if (elapsedTime > 3f)
        {
            Color newColor = playerSprite.color;
            newColor.a = 1f;
            playerSprite.color = newColor;
        }


        // Stops the dash and allows the player to use the rest of the inputs
        if (elapsedTime > dashingTime)
        {
            rb.gravityScale = 9.8f;
            isDashing = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("Holaaaaa");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
