using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
public class AnimalScript : MonoBehaviour
{
    //vars
    public float[] speed;
    public GameObject[] prefabs;

    private bool isAlive = true;
    private float boundsZ = 30;
    private ObjectReferenceManager objectReferenceManager;
    private float animalSpeed;
    void Start()
    {
        // Get our Reference manager
        objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();

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
        if (transform.position.z > boundsZ || transform.position.z < -(boundsZ - 20))
        {
            Destroy(gameObject);
            objectReferenceManager.gameBehaviourManager.IncrementHealth(-1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            isAlive = false;
            Destroy(gameObject);
            Destroy(other.gameObject);
            objectReferenceManager.gameBehaviourManager.IncrementScore(1);
        }
    }
}
