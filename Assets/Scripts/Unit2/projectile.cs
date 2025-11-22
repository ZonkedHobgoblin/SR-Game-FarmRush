using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class projectile : MonoBehaviour
{
    //vars
    public float[] speed;
    private float bound = 30;
    public Mesh[] model;
    public string[] anims;
    public bool isAnimal;
    private float animalSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        //randomize and model so no need for multiple prefabs
        int ranNum = Random.Range(0, model.Length);
        GetComponent<MeshFilter>().mesh = model[ranNum];
        //set speed from speed array
        animalSpeed = speed[ranNum];
        //check if its an animal, if yes then play anim from array (same script used for projectile so)
        if (isAnimal == true)
        {
            GetComponent<Animator>().Play(anims[ranNum]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * animalSpeed);
        //delete the object when too far
        if (transform.position.z > bound || transform.position.z < -(bound - 20))
        {
            Destroy(gameObject);
        }
    }
}
