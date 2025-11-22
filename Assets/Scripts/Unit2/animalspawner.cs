using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalspawner : MonoBehaviour
{
    //vars
    public GameObject animalprefab;
    public float spawnrate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnAnimal", 2, Random.Range(0.5f, 3f));
    }

    // Update is called once per frame
    void SpawnAnimal()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-13, 13), 0, 20);
        Instantiate(animalprefab, spawnPos, new Quaternion(0, -180, 0, 0));
    }
}
