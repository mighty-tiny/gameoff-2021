using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /* PLAYER */
    private PlayerControls playerControls; // Reference Player Controller.
    private CharacterController characterController; // Reference CharacterController
    private Animator anim;
    private Vector3 velocity;
    private float horizontalInput;
    private float verticalInput;
    private float ySpeed;
    private bool isSprinting;
    private Vector2 move;
    private bool canSwing = true;

    /* SERIALIZED */
    [SerializeField] private GameObject player;
    [SerializeField] LayerMask groundLayers;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float walkSpeed; // Walk Speed.  Not yet implemented.
    [SerializeField] private float runSpeed; // Run Speed
    [SerializeField] private float jumpHeight = 2; // Jump Height
    [SerializeField] private Transform cameraTransform; // Camera Rotation

    /* GRAVITY */
    private float gravity = -50f;
    private bool isGrounded;
    private bool isJumping;


    private void Awake()
    {
        playerControls = new PlayerControls(); // Instantiate controlls.

        /* Sprint */
        playerControls.Land.SprintStart.performed += x => SprintPressed();
        playerControls.Land.SprintEnd.performed += x => SprintReleased();

        /* Attack */
        playerControls.Land.Attack.performed += _ => AttackPressed();
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
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        playerJumpandGravity();

        playerMovement();
    }

    private void playerJumpandGravity()
    {
        /* Jumping / Gravity */
        // Is the player touching the ground?
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundLayers, QueryTriggerInteraction.Ignore);
        isJumping = playerControls.Land.Jump.triggered;

        // Apply Gravity if hero is not grounded.
        if (isGrounded && isJumping)
        {
            anim.SetBool("Grounded", false);
            // velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            ySpeed += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else if (isGrounded)
        {
            anim.SetBool("Grounded", true);
            // velocity.y = 0.0f;
            ySpeed = velocity.y = 0.0f;
        }
        else
        {
            // velocity.y += gravity * Time.deltaTime;
            ySpeed += gravity * Time.deltaTime;
        }
    }

    private void playerMovement()
    {
        // Movement
        move = playerControls.Land.Move.ReadValue<Vector2>();

        horizontalInput = move.x;
        verticalInput = move.y;

        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput);

        // Speed?
        if (isGrounded)
        {
            if (movementDirection != Vector3.zero)
            {
                if (!isSprinting)
                {
                    // Walk
                    Walk();
                }
                else if (isSprinting)
                {
                    // Run
                    Run();
                }
            }
            else if (movementDirection == Vector3.zero)
            {
                // Idle
                Idle();
            }
        }
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();


        velocity = movementDirection * movementSpeed;
        velocity.y = ySpeed;

        if (movementDirection != Vector3.zero)
        {
            transform.forward = movementDirection;
        }

        characterController.Move(velocity * Time.deltaTime);
    }

    private void SprintPressed()
    {
        isSprinting = true;
    }

    private void SprintReleased()
    {
        isSprinting = false;
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        movementSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        movementSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void AttackPressed()
    {
        if (canSwing)
        {
            StartCoroutine(Attack());
            canSwing = false;
        }
    }

    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(.5f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        canSwing = true;
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
