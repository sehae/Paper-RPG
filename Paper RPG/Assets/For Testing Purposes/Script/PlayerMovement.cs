using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float fallMultiplier = 2.5f; // For faster falling

    private Vector3 velocity;
    private bool isGrounded;
    private bool facingRight = true;

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        // Get input from the keyboard
        float moveDirectionX = Input.GetAxis("Horizontal");
        float moveDirectionY = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 move = new Vector3(moveDirectionX, 0, moveDirectionY);
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

        // Handle grounded state and reset velocity.y
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small value to keep the player grounded
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate the jump velocity
            float jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            velocity.y = jumpVelocity;
        }

        // Apply custom gravity
        if (velocity.y < 0)
        {
            // Apply fallMultiplier when falling
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Apply gravity and move the character
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
