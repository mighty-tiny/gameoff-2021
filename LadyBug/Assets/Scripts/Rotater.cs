using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    [HideInInspector]
    public Vector3 startPos;
    //
    [SerializeField] bool Float = true, rotate = true;

    [SerializeField] float floatAmplitude = 1, floatSpeed = 1;


    [SerializeField] Vector3 rotationSpeed = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        if (Float)
        {
            transform.position += new Vector3(0, (Mathf.Sin(Time.time * floatSpeed) * floatAmplitude) * Time.deltaTime);
        }
    }
}
