using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameBehaviourManager : MonoBehaviour
{
    public event Action OnGameover;


    private ObjectReferenceManager objectReferenceManager;


    void Start()
    {
        // Get our Object Reference Manager
        objectReferenceManager = GetComponent<ObjectReferenceManager>();

        // Start spawning animals by calling the function to invoke repeating of spawn func
        objectReferenceManager.animalSpawnManager.StartSpawning();

        // Load high score and set it on GameStateManager
        objectReferenceManager.stateManager.SetHighScore(objectReferenceManager.saveManager.LoadData());
    }

    void Update()
    {
        // Highscore management
        if (objectReferenceManager.stateManager.GetScore() > objectReferenceManager.stateManager.GetHighScore())
        {
            objectReferenceManager.stateManager.SetHighScore(objectReferenceManager.stateManager.GetScore());
            objectReferenceManager.saveManager.SaveData(objectReferenceManager.stateManager.GetHighScore());
        }

        // Gameover management
        if ((objectReferenceManager.stateManager.GetHealth() >= 0) && (!objectReferenceManager.stateManager.GetIsGameover()))
        {
            objectReferenceManager.stateManager.SetGameover(true);
            OnGameover?.Invoke();
        }


    }
}
