using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{    
    // Start is called before the first frame update
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    //UserInput userInput = new UserInput();
    //userInput.Player.Enable();


    public float moveSpeed = 0f;
    public float maxSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float accelerationSpeed = 20f;
    public float decelerationSpeed = 30f;
    public float direction_face_x = 0f;
    public float direction_face_z = 0f;
    public Vector2 inputVector;
    public Vector2 last_direction;
    Vector3 velocity;
    public bool isGrounded;
    public Vector2 starting_vector;
    public float direction_magnitude = 0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float start_value_x;
    float start_value_y;
    float timeElapsed;


    void Start(){
        StartCoroutine(DirectionChange());
    }

    void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        anim.SetBool("Grounded", isGrounded);
        
        
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //if(inputVector.x > momentum.x && inputVector.x > .1f){
        //    momentum.x += Time.deltaTime * accelerationSpeed;
        //}
        //else if(inputVector.x > momentum.x){
        //    momentum.x += Time.deltaTime * decelerationSpeed;
        //}
        //if(inputVector.x < momentum.x && inputVector.x < -.1f){
        //    momentum.x -= Time.deltaTime * accelerationSpeed;
        //}
        //else if(inputVector.x < momentum.x){
        //    momentum.x -= Time.deltaTime * decelerationSpeed;
        //}
        //
        //if(inputVector.y > momentum.y && inputVector.y > .1f){
        //    momentum.y += Time.deltaTime * accelerationSpeed;
        //}
        //else if(inputVector.y > momentum.y){
        //    momentum.y += Time.deltaTime * decelerationSpeed;
        //}
        //if(inputVector.y < momentum.y && inputVector.y < -.1f){
        //    momentum.y -= Time.deltaTime * accelerationSpeed;
        //}
        //else if(inputVector.y < momentum.y){
        //    momentum.y -= Time.deltaTime * decelerationSpeed;
        //}
        //
        //if(momentum.x > 1f){
        //    momentum.x = 1f;
        //}
        //if(momentum.x < -1f){
        //    momentum.x = -1f;
        //}
        //if(Mathf.Abs(momentum.x) < .1){
        //    momentum.x = 0f;
        //}
        //if(momentum.y > .9f){
        //    momentum.y = 1f;
        //}
        //if(momentum.y < -.9f){
        //    momentum.y = -1f;
        //}
        //if(Mathf.Abs(momentum.y) < .1f){
        //    momentum.y = 0f;
        //}

        //float delta_x = inputVector.x - momentum.x;
        //delta_x *= Time.deltaTime;
        //momentum.x += delta_x;

        //if(inputVector.x > momentum.x && inputVector.x > .1f){
        //    if(timeElapsed < accelerationSpeed){
        //        momentum.x = Mathf.Lerp(momentum.x, inputVector.x, timeElapsed / accelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //else if(inputVector.x > momentum.x){
        //    if(timeElapsed < decelerationSpeed){
        //        momentum.x = Mathf.Lerp(momentum.x, inputVector.x, timeElapsed / decelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(inputVector.y > momentum.y && inputVector.y > .1f){
        //    if(timeElapsed < accelerationSpeed){
        //        momentum.y = Mathf.Lerp(momentum.y, inputVector.y, timeElapsed / accelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(inputVector.y > momentum.y){
        //    if(timeElapsed < decelerationSpeed){
        //        momentum.y = Mathf.Lerp(momentum.y, inputVector.y, timeElapsed / decelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(inputVector.x < momentum.x && inputVector.x < -.1f){
        //    if(timeElapsed < accelerationSpeed){
        //        momentum.x = Mathf.Lerp(momentum.x, inputVector.x, timeElapsed / accelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(inputVector.x < momentum.x){
        //    if(timeElapsed < decelerationSpeed){
        //        momentum.x = Mathf.Lerp(momentum.x, inputVector.x, timeElapsed / decelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(inputVector.y < momentum.y && inputVector.y <-.1f){
        //    if(timeElapsed < accelerationSpeed){
        //        momentum.y = Mathf.Lerp(momentum.y, inputVector.y, timeElapsed / accelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(inputVector.y < momentum.y){
        //    if(timeElapsed < decelerationSpeed){
        //        momentum.y = Mathf.Lerp(momentum.y, inputVector.y, timeElapsed / decelerationSpeed);
        //        timeElapsed += Time.deltaTime;
        //    }
        //}
        //if(momentum.x > .95f){
        //    momentum.x = 1f;
        //}
        //if(momentum.x < -.95f){
        //    momentum.x = -1f;
        //}
        //if(Mathf.Abs(momentum.x) < .001f){
        //    momentum.x = 0f;
        //}
        //if(momentum.y > .95f){
        //    momentum.y = 1f;
        //}
        //if(momentum.y < -.95f){
        //    momentum.y = -1f;
        //}
        //if(Mathf.Abs(momentum.y) < .001f){
        //    momentum.y = 0f;
        //}
        //momentum = Vector2.Lerp(momentum, inputVector, Time.deltaTime);
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        
        

        //if(direction.magnitude >= 0.1f)
        //{
        
        Vector3 direction = new Vector3(inputVector.x, 0f, inputVector.y);
        direction_magnitude = Mathf.Clamp01(direction.magnitude);
        direction.Normalize();
        
        
        if(direction_magnitude >= 0.1f){
            direction_face_x = direction.x;
            direction_face_z = direction.z;
        }
        float targetAngle = Mathf.Atan2(direction_face_x, direction_face_z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        if(direction_magnitude >= 0.1f && moveSpeed < direction_magnitude * maxSpeed){
            moveSpeed += accelerationSpeed * Time.deltaTime;
        }
        else if(direction_magnitude < 0.1f && moveSpeed > direction_magnitude * maxSpeed){
            moveSpeed -= decelerationSpeed * Time.deltaTime;
        }
        if(moveSpeed > maxSpeed - .1f){
            moveSpeed = maxSpeed;
        }
        if(moveSpeed < 0.1f){
            moveSpeed = 0f;
        }
        anim.SetFloat("MoveSpeed", moveSpeed);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
        //}
    }
    void OnJump()
    {
        if(isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        Debug.Log("Jump");
    }
    

    IEnumerator DirectionChange() {
        while(true){
            Vector2 last_direction = inputVector;
            //Debug.Log(last_direction);
            yield return null;
            if (Vector2.Dot(last_direction.normalized, inputVector.normalized) < 0f && moveSpeed > 4f) { 
                Debug.Log(Vector2.Dot(last_direction, inputVector));
                Debug.Log("Direction Changed!"); 
                //last_direction = inputVector;
                moveSpeed = 0f;
            }
            //yield return new WaitForSeconds(1f);
        }
            //last_direction = inputVector;
    }

 }
