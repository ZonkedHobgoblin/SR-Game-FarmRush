using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{
    //vars
    public float speed = 40.0f;
    private float bound = 30;
    public Mesh[] model;
    // Start is called before the first frame update
    void Start()
    {
        //randomize and model so no need for multiple prefabs
        int ranNum = Random.Range(0, model.Length);
        GetComponent<MeshFilter>().mesh = model[ranNum];
        // to do: fix size
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //delete the object when too far
        if (transform.position.z > bound || transform.position.z < -(bound - 20))
        {
            Destroy(gameObject);
        }
    }
}
