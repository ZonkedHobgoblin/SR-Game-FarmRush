using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
    //vars
    public GameObject animalprefab;


    // Invoke a reapeating call of the SpawnAnimal() at a random interval between 0.5 and 3 seconds
    public void StartSpawning()
    {
        InvokeRepeating("SpawnAnimal", 2, Random.Range(0.5f, 3f));
    }

    // Spawn the animal prefab at a random postion - on Z 20 between a random point of -13 and 13 on X
    void SpawnAnimal()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-13, 13), 0, 20);
        Instantiate(animalprefab, spawnPos, new Quaternion(0, -180, 0, 0));
    }
}
