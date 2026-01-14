using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //vars
    public float speed = 45f;
    private float bound = 30;
    public Mesh[] model;
    void Start()
    {
        //randomize from array to select model
        int ranNum = Random.Range(0, model.Length);
        GetComponent<MeshFilter>().mesh = model[ranNum];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //delete the object when too far
        if (transform.position.z > bound || transform.position.z < -(bound - 20) || transform.position.y > 5 || transform.position.y < -4)
        {
            Destroy(gameObject);
        }
    }
}
