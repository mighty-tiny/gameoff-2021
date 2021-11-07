using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    Vector2 movementInput;
    CharacterController controller;
    public float speed = 1;
    [SerializeField] Vector3 gravity = new Vector3(0, -10, 0);

    Vector3 velocity;

    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    [SerializeField] float jumpHeight = 3;
    // Start is called before the first frame update


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;

        controller.Move(move * speed * Time.deltaTime);

        velocity += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnMovement(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            Debug.Log("Jumped");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity.y);
        }
    }





}