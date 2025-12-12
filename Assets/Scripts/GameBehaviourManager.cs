using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameBehaviourManager : MonoBehaviour
{
    public event Action OnGameover;



    private GameStateManager stateManager;
    private SaveManager saveManager;
    private AnimalSpawnManager animalSpawnManager;  


    void Start()
    {
        // Get Game objects and scripts
            // Get managers from our game object
                // Get GameStateManager
                stateManager = gameObject.GetComponent<GameStateManager>();
                // Get SaveManager
                saveManager = gameObject.GetComponent<SaveManager>();

            // Get other objects/components not on our object
                // Get AnimalSpawnManager
                animalSpawnManager = FindFirstObjectByType<AnimalSpawnManager>();


        // Start spawning animals by calling the function to invoke repeating of spawn func
            // Call spawn function on AnimalSpawnManager
            animalSpawnManager.StartSpawning();


        // Saving and loading of high score
            // Load high score and set it on GameStateManager
            stateManager.SetHighScore(saveManager.LoadData());
    }

    // Update is called once per frame
    void Update()
    {
        // Highscore management
        if (stateManager.GetScore() > stateManager.GetHighScore())
        {
            stateManager.SetHighScore(stateManager.GetScore());
            saveManager.SaveData(stateManager.GetHighScore());
        }

        // Gameover management
        if ((stateManager.GetHealth() >= 0) && (!stateManager.GetIsGameover()))
        {
            stateManager.SetGameover(true);
            OnGameover?.Invoke();
        }


    }
}
