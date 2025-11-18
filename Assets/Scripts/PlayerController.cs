using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxSpeed;
    public float acceleration = 20f;
    private FacingDirection currentFacing = FacingDirection.right;

    public float rayHeigh;
    public LayerMask groundLayer;
    public Color rayColor;

   // public float jumpForce;
    public float apexHeight = 1f;
    public float apexTime=0.5f;
    public float initialJumpVelocity;
    public float gravity;
    public Rigidbody2D controllerRB;
    private bool jumpTrigger = false;
    public float terminalFallSpeed = -5f;

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.gravityScale = 0;
        gravity = -2 * apexHeight / (Mathf.Pow(apexTime,2));
        initialJumpVelocity = 2 * apexHeight / apexTime; 

    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        MovementUpdate(playerInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                jumpTrigger = true;
            }

        }

    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (rb == null) {return;}

        Vector2 targetVelocity = new Vector2(playerInput.x * maxSpeed, rb.linearVelocity.y);

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity,targetVelocity,acceleration * Time.fixedDeltaTime);
        if (playerInput.x > 0.1f)
        {
            currentFacing = FacingDirection.right;
        }
        else if (playerInput.x < -0.1f)
        {
            currentFacing = FacingDirection.left;
        }



    }
    public void FixedUpdate()
    {
        controllerRB.linearVelocityY += gravity * Time.fixedDeltaTime;
        if (jumpTrigger)
        {
            

            controllerRB.linearVelocityY = initialJumpVelocity;
            jumpTrigger = false;

        }
        if (controllerRB.linearVelocityY < terminalFallSpeed)
        {
            controllerRB.linearVelocityY = terminalFallSpeed;

        }

    }
    public bool IsWalking()
    {
        
        return false;
    }
    public bool IsGrounded()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin,Vector2.down, rayHeigh,groundLayer);
        
        if (hit) { rayColor = Color.green; } else {  rayColor = Color.red; }
        Debug.DrawRay(rayOrigin,Vector2.down * rayHeigh, rayColor);

        return hit.collider != null;
    }

    public FacingDirection GetFacingDirection()
    {
        return currentFacing;
    }

   /*public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, initialJumpVelocity);
    }
  
    */
}
