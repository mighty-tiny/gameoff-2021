using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rotation : MonoBehaviour
{   
    private Vector2 mouse;

    public Vector2 mouseSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * mouse.x);
    }

    void OnRotation(InputValue value)
    {
        mouse = value.Get<Vector2>() * mouseSensitivity * Time.deltaTime;
    }
}

