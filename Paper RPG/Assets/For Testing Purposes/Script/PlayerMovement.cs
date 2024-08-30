using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool facingRight = true; // Track which direction the player is facing

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        // Get input from the keyboard
        float moveDirectionX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow

        // Calculate movement vector
        Vector3 move = new Vector3(moveDirectionX, 0, 0); // Horizontal movement only
        controller.Move(move * speed * Time.deltaTime);

        // Flip the sprite based on movement direction
        if (moveDirectionX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirectionX < 0 && facingRight)
        {
            Flip();
        }

        // Handle jumping
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // A small value to keep the player grounded
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight; // Toggle the direction

        // Flip the sprite by changing the x scale
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
