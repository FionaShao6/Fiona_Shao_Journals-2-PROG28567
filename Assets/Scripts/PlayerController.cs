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

    public float coyoteTime = 0.1f;
    public float coyoteTimeCounter;

    public float dashMultiplier = 2f; // dash speed multiplier
    public float dashDuration = 0.3f; 
    public float dashCooldown = 1f; // dash cooldown time£¬ 1 second, to prevent infinite dashing
    public float dashCooldownTimer = 0f;//Current remaining cooldown time
    private bool isDashing = false; // Is the player dashing?
    public LayerMask enemyLayer; // used to set penetration
    public float targetSpeed;

   

    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controllerRB =GetComponent<Rigidbody2D>();
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


        if (IsGrounded())
        {
            
            coyoteTimeCounter = 0f;
        }
        else
        {
            coyoteTimeCounter += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded() || coyoteTimeCounter < coyoteTime)
            {
                jumpTrigger = true;

            }

        }

        bool isHoldingDirection = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f;
        //Whether it's to the left or right, it can be detected using absolute values.

        bool isPressingShift = Input.GetKeyDown(KeyCode.LeftShift);

        bool canDash = !isDashing && isHoldingDirection&& dashCooldownTimer <= 0f;
        //Check if you can dash: Not currently dashting + Hold down the direction key
      
       
        if (isPressingShift && canDash)
        {
            StartDash();
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (rb == null) {return;}

        if (isDashing == true)
        {
            targetSpeed = maxSpeed * dashMultiplier;
        }
        else
        {
            targetSpeed = maxSpeed ;
        }
        Vector2 targetVelocity = new Vector2(playerInput.x * targetSpeed, rb.linearVelocity.y);

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


    public void StartDash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;
        //Activating the cooldown is equivalent to dashing,
        //the cooldown immediately becomes 1 second and the countdown begins.
        
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerIndex, true);
        Invoke("EndDash", dashDuration);
        //The EndDash() method will be executed automatically after dashDuration seconds.

    }
    public void EndDash()
    {
        isDashing = false;
        int enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerIndex, false);
        Debug.Log("finish dash");
       
    }

}
