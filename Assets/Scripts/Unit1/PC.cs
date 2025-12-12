using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // variables
    private float speed = 15.0f;
    private float turnSpeed = 45.0f;
    private float horizontalInput;
    private float forwardInput;
    void Update()
    {
        //get inputs
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        //forward stuff
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //rotational stuff
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);

    }
}
