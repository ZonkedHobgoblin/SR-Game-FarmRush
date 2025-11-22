using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
public class animal : MonoBehaviour
{
    //vars
    public float[] speed;
    private float bound = 30;
    public GameObject[] prefabs;
    private float animalSpeed = 4.0f;
    private GameObject spawnedmodel;
    // ienumerator cause unity too slow with the anim stuff
    void Start()
    {
        //disable mesh renderer and filter (Visual of model for editor)
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshFilter>().mesh = null;
        //model setup VVVVVVVVVVVVVV
        //get ran num from array
        int ranNum = Random.Range(0, prefabs.Length);
        //spawn the prefab
        GameObject spawnedModel = Instantiate(prefabs[ranNum], transform);
        //randomize from array to select model
        animalSpeed = speed[ranNum];
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
