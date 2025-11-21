using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class pc2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float horizontalInput;
    public float speed = 10.0f;
    public float Range = 10;
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Clam
    }
}
