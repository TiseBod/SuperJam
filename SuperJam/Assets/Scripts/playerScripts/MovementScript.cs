using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Transform mainCameraTransform;
    PlayerInput playerInput;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    CharacterController characterController;
    Animator animator;
    private float rotationFactorPerFrame = 10f;
    private bool isRunPressed;
  

    private int isWalkingHash;
    
    private int isRunningHash;
    private int isJumpingHash;
    private int isDoubleJumpingHash;
    private bool isJumpAnimating;

    private float targetRotationAngle;
    
    public float walkMultiplier = 1.5f;

    public float runMultiplier = 3.0f;


    private int zero= 0;
    public float gravity = -9.8f;
    private float groundedGravity = -0.5f;
    
    
    

    private bool isJumpPressed = false;
    private float initialJumpVelocity;
    private float maxJumpVelocity;
    private float maxJumpHeight = 4.0f;
    private float maxJumptime = 0.75f;
    float secondJumpHeight = 4.0f;
    bool isJumping = false;
    private bool isDoubleJumping = false;
    //private bool isFloating = false;
    private int isFloatingHash;
    
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] hitClip;
    
    
    

    void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isDoubleJumpingHash = Animator.StringToHash("isDoubleJump");
        isFloatingHash = Animator.StringToHash("isFloating");
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); 

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.performed += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;
        playerInput.CharacterControls.Jump.performed += onJump;
        // TODO: hopefully we can figure out a way to move this cursor locking behaviour to some place that makes sense
        // It is also currently present in the InventoryManager script to make this change not break UI interaction
        //Cursor.lockState = CursorLockMode.Locked; // Lock cursor to make 3rd person view work well
        setUpJumpVariables();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
      
    }

    void setUpJumpVariables()
    {
        float timeToApex = maxJumptime / 2;
        gravity = (-2*maxJumpHeight)/Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            animator.SetBool(isJumpingHash, true);
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
            isJumpAnimating = true;
            audioSource.clip = hitClip[0];
            audioSource.Play();
            
            
            Debug.Log($"Did I jump?  {initialJumpVelocity} value");
            currentMovement.y = initialJumpVelocity * .5f;
           // gravityMovement.y += initialJumpVelocity;
        }else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false; 
        }
    }

    void handleDoubleJump()
    {
        if (isJumping && Input.GetButton("Fire1") && !isDoubleJumping && !characterController.isGrounded)
        {
            isDoubleJumping = true;
            Debug.Log("double jumps");
            animator.SetBool(isDoubleJumpingHash,true);
           audioSource.clip = hitClip[1];
            audioSource.Play();
            
            currentMovement.y = initialJumpVelocity * .75f;
        }else if(!isJumpPressed && isDoubleJumping && characterController.isGrounded)
        {
            isDoubleJumping = false;
        }
    }

    

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        Debug.Log(isJumpPressed + "just jumped" );
        
    }



    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
        Debug.Log(isRunPressed);
    }

    private float rotate(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0)
        {
            directionAngle += 360;
        }

        // Add the main camera's orientation to make the rotation relative
        directionAngle += mainCameraTransform.eulerAngles.y;

        if (directionAngle > 360)
        {
            directionAngle -= 360;
        }
       

        return directionAngle;
    }

    void handleRotation()
    {
        Vector3 postionToLookAt;
        postionToLookAt.x = currentMovement.x;
        postionToLookAt.y = 0.0f;
        postionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            // Update global variable to pass to use in move function
            targetRotationAngle = rotate(postionToLookAt);
            Quaternion targetRotation = Quaternion.Euler(0f, targetRotationAngle, 0f);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
    }
    void handleAnimation()
    {
        if (animator == null) return; // Prevent errors

        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        } 

        if ((isMovementPressed && isRunPressed) && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
            
        }
    }

    void handleGravity()
    {

        bool isFalling = currentMovement.y <= 0.0f || !isJumpPressed;

        float fallMultiplier;
        if (isDoubleJumping && isFalling && Input.GetKey(KeyCode.C))
        {
            fallMultiplier = 0.3f;
            Debug.Log("floating");
            animator.SetBool(isFloatingHash, true);
            
        }
        else
        {
            fallMultiplier = 1.5f;
            animator.SetBool(isFloatingHash, false);
        }

       
        
        
        
       
        if (characterController.isGrounded && currentMovement.y < 0f)
        {
            
        
            if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                animator.SetBool(isDoubleJumpingHash, false);
               
                
                isJumpAnimating = false;
            }

            currentMovement.y = groundedGravity;
           // isJumping = false;
        }else if(isFalling)
        {
             float previousYVelocity = currentMovement.y;
             float newYVelocity = currentMovement.y + (gravity * fallMultiplier*Time.deltaTime);
             float nextYVelocity = Mathf.Max((previousYVelocity + newYVelocity) * 0.5f, -20f);
             currentMovement.y = nextYVelocity;
              
             
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            currentMovement.y = nextYVelocity;
          //  currentMovement.y += gravity * Time.deltaTime;
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        isMovementPressed = currentMovementInput.magnitude > 0;

        // Preserve current Y (jump/gravity)
        float currentY = currentMovement.y;
        currentMovement = new Vector3(currentMovementInput.x, currentY, currentMovementInput.y);
    }


    void move()
    {
        Vector3 horizontalMovement = Vector3.zero;

        if (isMovementPressed)
        {
            Vector3 moveDir = Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
            float speed = isRunPressed ? runMultiplier : walkMultiplier;
            horizontalMovement = moveDir * speed;
        }

        Vector3 totalMovement = horizontalMovement + new Vector3(0f, currentMovement.y, 0f);
        characterController.Move(totalMovement * Time.deltaTime);
    }


    void Update()
    {
        handleGravity();
        handleJump();         // 👈 move this BEFORE move()
        handleDoubleJump();
        handleAnimation();
        handleRotation();
        move();

    }

    void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}
