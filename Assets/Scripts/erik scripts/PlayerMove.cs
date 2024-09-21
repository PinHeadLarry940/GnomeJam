using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;


    public float groundDrag;


    public float jumpForce;
    public float jumpCooldown;
    public float airMult;
    bool readyToJump;
    //public KeyCode jumpKey = KeyCode.Space;
            
    public float playerHeight;
    public LayerMask watGround;
    bool grounded;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDir;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   
        readyToJump = true;
    }


    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, watGround);

        MyInput();
        SpeedControl();

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
        if(Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            Debug.Log("I jump");
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }



    private void MovePlayer()
    {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;


        //on ground 

        if(grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 5f, ForceMode.Force);

        //in air
        else if(!grounded)
            rb.AddForce(moveDir.normalized * moveSpeed * 5f * airMult, ForceMode.Force);
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);

        }

    }



    private void Jump()
    {

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {

        readyToJump = true;

    }
}
