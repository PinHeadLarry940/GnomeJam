using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float crouchSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool readyToJump;

    
    public float crouchYscale;
    private float startYscale;

    //keys
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public float playerHeight;
    public LayerMask watGround;
    bool grounded;


    public float maxSlope;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDir;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air

    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   
        readyToJump = true;

        startYscale = transform.localScale.y;
    }


    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, watGround);

        MyInput();
        SpeedControl();
        StateHandler();



        //handle drag

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //Debug.Log("ready to jump" + readyToJump);
        Debug.Log("am i on ground?" + grounded);
        //Debug.DrawRay()
    }
    private void MyInput()
    {

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Debug.Log("I jump");
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYscale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch

        if (Input.GetKeyUp(crouchKey)) 
        {
            transform.localScale = new Vector3(transform.localScale.x, startYscale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        //mode crouch
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }



        // mode sprint
        else if(grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        //mode walk
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;

        }
        //mode air
        else
        {
            state = MovementState.air;
        }
    
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }



    private void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on sloep

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDir() * moveSpeed *20f, ForceMode.Force);

            if(rb.velocity.y > 0)
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }


        //on ground 

        if(grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 5f, ForceMode.Force);

        //in air
        else if(!grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 5f * airMult, ForceMode.Force);
    
    
    //turn off grav on slope

        rb.useGravity = !OnSlope();
    
    
    }
    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {

            if(rb.velocity.magnitude > moveSpeed)
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

    private void ResetJump()
    {

        readyToJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle > maxSlope && angle != 0;
        }
        return false;  
       
    }

    private Vector3 GetSlopeMoveDir()
    {

        return Vector3.ProjectOnPlane(moveDir,slopeHit.normal).normalized;

    }
}
