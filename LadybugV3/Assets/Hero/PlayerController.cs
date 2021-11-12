using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /* PLAYER */
    private PlayerControls playerControls; // Reference Player Controller.
    private CharacterController characterController;
    private Vector3 velocity;
    private float horizontalInput;
    private float verticalInput;
    private float ySpeed;
  
    /* SERIALIZED */
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float walkSpeed = 2; // Walk Speed.  Not yet implemented.
    [SerializeField] private float runSpeed = 5; // Run Speed
    [SerializeField] private float jumpHeight = 2; // Jump Height
    [SerializeField] private Transform cameraTransform; // Camera Rotation

    /* GRAVITY */
    private float gravity = -50f;
    private bool isGrounded;
    private bool isJumping;
    

    private void Awake()
    { 
        playerControls = new PlayerControls(); // Instantiate controlls.
    }

    
    private void OnEnable() // OnEnable is called when Script is Enabled.
    {
        playerControls.Enable(); // Enable Action Map Inputs.
    }

    
    private void OnDisable() // OnDisable is called when Script is Disabled.
    {
        playerControls.Disable(); // Set up Action disable.
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Get the CharacterController component to use in this script.
    }

    private void Update()
    {

        /* Jumping / Gravity */
        // Is the player touching the ground?
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);
        isJumping = playerControls.Land.Jump.triggered;

        // Apply Gravity if hero is not grounded.
        if (isGrounded && isJumping)
        {
            // velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            ySpeed += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else if (isGrounded)
        {
            // velocity.y = 0.0f;
            ySpeed = velocity.y = 0.0f;
        }
        else
        {
            // velocity.y += gravity * Time.deltaTime;
            ySpeed += gravity * Time.deltaTime;
        }

        // Movement
        Vector2 move = playerControls.Land.Move.ReadValue<Vector2>();
        horizontalInput = move.x;
        verticalInput = move.y;
        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        velocity = movementDirection * runSpeed;
        velocity.y = ySpeed;

        // characterController.Move(new Vector3(horizontalInput * runSpeed, 0.0f, verticalInput * runSpeed) * Time.deltaTime);
        // characterController.Move(velocity * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
