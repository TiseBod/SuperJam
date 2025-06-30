using UnityEngine;
using UnityEngine.InputSystem;

public class JamoMove : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Transform mainCameraTransform;
    PlayerInput playerInput;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;
    CharacterController characterController;
    Animator animator;
    private float rotationFactorPerFrame = 10f;
    private bool isRunPressed;
    private Vector3 gravityMovement;

    private int isWalkingHash;
    
    private int isRunningHash;

    private float targetRotationAngle;
    
    public float walkMultiplier = 1.5f;

    public float runMultiplier = 3.0f;

    void Awake()
    {
        mainCameraTransform = Camera.main.transform;
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>(); 

        playerInput.CharacterControls.Move.started += onMovementInput;
        playerInput.CharacterControls.Move.canceled += onMovementInput;
        playerInput.CharacterControls.Move.performed += onMovementInput;
        playerInput.CharacterControls.Run.started += onRun;
        playerInput.CharacterControls.Run.canceled += onRun;
        // TODO: hopefully we can figure out a way to move this cursor locking behaviour to some place that makes sense
        // It is also currently present in the InventoryManager script to make this change not break UI interaction
        //Cursor.lockState = CursorLockMode.Locked; // Lock cursor to make 3rd person view work well
       
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
        if (characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            gravityMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -9.8f;
            gravityMovement.y += gravity * Time.deltaTime;
        }
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>(); // Read input values correctly
        currentMovement = new Vector3(currentMovementInput.x, 0f, currentMovementInput.y);
        isMovementPressed = currentMovementInput.magnitude > 0;
    }

    void move()
    {
        // Gravity movement
        characterController.Move(gravityMovement * Time.deltaTime);
        
        if (isMovementPressed)
        {
            // Move player relative to the camera's rotation
            Vector3 moveDir = Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
            if (isRunPressed)
            {
                characterController.Move(moveDir * (runMultiplier * Time.deltaTime));
            }
            else
            { 
                characterController.Move(moveDir * (walkMultiplier * Time.deltaTime));
            }
        }
    }

    void Update()
    {
        handleGravity();    
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

