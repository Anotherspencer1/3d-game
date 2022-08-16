using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("References")]
    Rigidbody rb;
    customGravity gravity;
    public Transform cam;
    public Transform orientation;
    public Transform playerObj;
    public Animator anim;

    [Header("Movement")]
    Vector2 inputVector;
    Vector3 moveDir;
    public float topSpeed = 10f;
    public float groundDrag = 5f;
    public float rotationSpeed = 5f;
    private int x = 0;
    private bool sprint;

    [Header("Jumping/Gravity")]
    public float jumpForce = 3f;
    public float jumpCooldown = .25f;
    public float airMultiplier;
    private bool readyToJump;
    public float fallGravityMultiplier = 4f;
    private float gravityScale;

    [Header("Ground Check")]
    public float playerHeight = 4f;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Movement")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    
    
    private void Start(){
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<customGravity>();
        rb.freezeRotation = true;
        readyToJump = true;
    }


    private void Update(){
        grounded = Physics.CheckSphere(transform.position, 0.35f, whatIsGround);
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("MoveSpeed", rb.velocity.magnitude);
        if(grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
        
        ThirdPersonCamera();

        Debug.DrawLine(transform.position, transform.position + GetSlopeMoveDirection() * 4, Color.blue);
        
    }


    void FixedUpdate()
    {
        MovePlayer();
        
        if (rb.velocity.y < 0 && !grounded)
        {
            gravity.gravityScale = fallGravityMultiplier;
        }
        else
        {
            gravity.gravityScale = 1f;
        }
        
    }


    private void OnMovement(InputValue value){
        inputVector = value.Get<Vector2>();
    }


    private void OnJump(){
        if(readyToJump && grounded){
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }


    private void OnSprint(InputValue value){
        if(inputVector.magnitude > .8f)
            sprint = value.isPressed;
        else
            sprint = false;
    }


    private void MovePlayer(){
        if(rb.velocity.magnitude < 0.1f)
        {
            x = 0;
        }
        else if(x < 50)
        { 
            x += 1;
        }
        if(sprint)
            topSpeed = 15;
        else
            topSpeed = 10;
        
        
        if(grounded){
            moveDir = orientation.forward * inputVector.y + orientation.right * inputVector.x;
            
            if(OnSlope()){
            rb.AddForce(GetSlopeMoveDirection() * (.2f * (Mathf.Pow(.95f, x + -118)) + 425) * topSpeed);
            }
            else{
                rb.AddForce(moveDir * (.2f * (Mathf.Pow(.95f, x + -118)) + 425) * topSpeed);
            }
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
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
    }


    private void Jump(){
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce * 150f, ForceMode.Impulse);
    }


    private void ResetJump(){
        readyToJump = true;
    }


    private void ThirdPersonCamera(){
        Vector3 viewDir = transform.position - new Vector3(cam.position.x, transform.position.y, cam.position.z);
        orientation.forward = viewDir.normalized;

        Vector3 inputDir = orientation.forward * inputVector.y + orientation.right * inputVector.x;

        if(grounded)
        {
            if(inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }    
    }
}

/*
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > topSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * topSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    
    private void MovePlayer(){
        moveDir = orientation.forward * inputVector.y + orientation.right * inputVector.x;
        if(grounded)
            rb.AddForce((moveDir * topSpeed * rb.mass) * 50f);
            //rb.AddForce(acceleration * rb.mass);
        else if(!grounded)
            rb.AddForce(moveDir * topSpeed * 50f * airMultiplier);
            //rb.AddForce(acceleration * rb.mass);
            
            Vector2 targetSpeed = inputVector * topSpeed;
            speedDif = (targetSpeed.x * targetSpeed.x + targetSpeed.y * targetSpeed.y) - (rb.velocity.x * rb.velocity.x + rb.velocity.z * rb.velocity.z);
            speedDif = targetSpeed.magnitude - rb.velocity.magnitude;
            accelRate = (targetSpeed.magnitude > 0.01f) ? accelerationSpeed : deccelerationSpeed;
            movement = Mathf.Clamp(Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif), -30, 30);
            rb.AddForce((moveDir * rb.mass * topSpeed) * movement);
            public float accelerationSpeed = 12f;
    public float deccelerationSpeed = 16f;
    public float velPower = .80f;
    public float movement;
    public float accelRate;
    public float speedDif;
*/