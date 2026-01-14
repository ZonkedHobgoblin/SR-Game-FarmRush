using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnManager : MonoBehaviour
{
    //vars
    public GameObject animalprefab;


    // Invoke SpawnAnimal() at 5 seconds in
    public void StartSpawning()
    {
        Invoke(nameof(SpawnAnimal), 5f);
    }

    // Spawn the animal prefab at a random postion - on Z 20 between a random point of -13 and 13 on X, then call itself again for recursive invoking
    void SpawnAnimal()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-13, 13), 0, 20);
        Instantiate(animalprefab, spawnPos, new Quaternion(0, -180, 0, 0));
        float nextSpawnTime = Random.Range(0.5f, 3f);
        Invoke(nameof(SpawnAnimal), nextSpawnTime);
    }
}
