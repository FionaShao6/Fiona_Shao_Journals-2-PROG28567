using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxSpeed;
    public float acceleration = 20f;
    private FacingDirection currentFacing = FacingDirection.right;
    public enum FacingDirection
    {
        left, right
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        //The input from the player needs to be determined and then passed in the to the MovementUpdate which should
        //manage the actual movement of the character.
        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"),0);
        MovementUpdate(playerInput);



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
        
    }
    public bool IsWalking()
    {
        return false;
    }
    public bool IsGrounded()
    {
        return true;
    }

    public FacingDirection GetFacingDirection()
    {
        return currentFacing;
    }

   
}
