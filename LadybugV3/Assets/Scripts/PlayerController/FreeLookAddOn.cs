
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookAddOn : MonoBehaviour
{
    Rotation _rotation;
    public bool InvertY = false;
    private CinemachineFreeLook _freeLookComponent;


    private void Awake()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
        _rotation = gameObject.GetComponentInParent<Rotation>();    
    }

    // Update the look movement each time the event is trigger
    public void OnLook(InputAction.CallbackContext context)
    {
        //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
        Vector2 lookMovement = context.ReadValue<Vector2>().normalized;
        lookMovement.y = InvertY ? -lookMovement.y : lookMovement.y;

        // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        lookMovement.x = lookMovement.x * 180f;

        //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        _freeLookComponent.m_XAxis.Value += lookMovement.x * _rotation.mouseSensitivity.x * Time.deltaTime;
        _freeLookComponent.m_YAxis.Value += lookMovement.y * _rotation.mouseSensitivity.y * Time.deltaTime;
    }
}