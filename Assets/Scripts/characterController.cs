using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    //Speed and stuff
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    //ground check system
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;
    public float groundDrag;

    //Jumping System
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool readyToJump;

    //Crouching
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    //Sloping
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    //WallRunning
    public GameObject beanCam;
    public float wallSpeed;
    public bool isTouchWall;
    private RaycastHit wallHit;


    //Keybinds
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    //Movement States
    public movementState state;
    public enum movementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
        isTouchWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Character Movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // on slope
        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

        //on ground
        else if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        //in air
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        //turn off gravity while on slope
        rb.useGravity = !OnSlope();

        //Ground Check Ray poggers
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        //I am not speed
        SpeedControl();

      
        //Wall or collison detection for wall running hopefully
        if (Physics.Raycast(transform.position, transform.forward, out wallHit, playerHeight * 0.3f))
        {
          
            isTouchWall = true;
            moveSpeed = wallSpeed;
            
            
        }
        else
        {
            isTouchWall = false;
        }

        if (isTouchWall = true && !grounded)
        {
            //make character stick to wall or soemthing like that IDK useGravity no work
            isTouchWall = false;
        }
        else
        {
            
            isTouchWall = false;
        }

            //Jumping
            if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
        //Start crouching
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        //Stop crouching
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }

        
        //State of Character
        stateHandler();
    }

    private void SpeedControl()
    {
        //limit slope speed
        if(OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }

        
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void resetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private void stateHandler()
    {
        if (Input.GetKey(crouchKey))
        {
            state = movementState.crouching;
            moveSpeed = crouchSpeed;
        }

        //Sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            state = movementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if(grounded)
        {
            state = movementState.walking;
            moveSpeed = walkSpeed;
        }
        else
        {
            state = movementState.air;
        }
        
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;

    }
}
