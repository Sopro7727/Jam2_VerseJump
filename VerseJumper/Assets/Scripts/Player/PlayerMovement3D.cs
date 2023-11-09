using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;
    private Rigidbody2D rb;
    private BoxCollider2D cl;

    [SerializeField]
    private LayerMask groundLayer; // Serialized field for ground layer

    [SerializeField]
    private Animator animator; // Serialized field for the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Player movement
        float moveInput = Input.GetAxis("Horizontal");

        // Separate the sprite flipping logic
        if (moveInput < 0)
        {
            // Flip the object horizontally when moving left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput > 0)
        {
            // Reset the scale when moving right
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Set animation based on key input
        bool isMoving = Mathf.Abs(moveInput) > 0;
        animator.SetBool("IsMoving", isMoving);
        if(isMoving){
            Debug.Log("move");
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jumping
        if (IsGrounded3() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            // Set IsJumping to true
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        // Set animation based on jumping state
        if (rb.velocity.y > 0)
        {
            animator.SetBool("IsJumping", true);
        }
        else if (rb.velocity.y < 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
    }
    
    private bool IsGrounded3()
    {
        if(!animator.GetBool("IsJumping") && !animator.GetBool("IsFalling")){
            return true;
        }
        else{
            return false;
        }
    }
}
