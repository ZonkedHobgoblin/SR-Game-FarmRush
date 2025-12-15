using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameBehaviourManager : MonoBehaviour
{
    public event Action OnGameover;
    public event Action OnDamage;
    public event Action OnScore;


    private ObjectReferenceManager objectReferenceManager;

    private void Awake()
    {
        // Get our Object Reference Manager
        objectReferenceManager = GetComponent<ObjectReferenceManager>();
    }
    void Start()
    {
        // Load high score and set it on GameStateManager
        objectReferenceManager.stateManager.SetHighScore(objectReferenceManager.saveManager.LoadData());

        // Start spawning animals by calling the function to invoke repeating of spawn func
        objectReferenceManager.animalSpawnManager.StartSpawning();
    }

    public void IncrementHealth(int health)
    {
        objectReferenceManager.stateManager.SetHealth(objectReferenceManager.stateManager.GetHealth() + health);
        OnDamage?.Invoke();
        CheckGameover();

    }

    public void IncrementScore(int score)
    {
        objectReferenceManager.stateManager.SetScore(objectReferenceManager.stateManager.GetScore() + score);
        OnScore?.Invoke();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (objectReferenceManager.stateManager.GetScore() > objectReferenceManager.stateManager.GetHighScore())
        {
            objectReferenceManager.stateManager.SetHighScore(objectReferenceManager.stateManager.GetScore());
            objectReferenceManager.saveManager.SaveData(objectReferenceManager.stateManager.GetHighScore());
        }
    }

    private void CheckGameover()
    {
        // Gameover management
        if ((objectReferenceManager.stateManager.GetHealth() <= 0) && (!objectReferenceManager.stateManager.GetIsGameover()))
        {
            objectReferenceManager.stateManager.SetGameover(true);
            OnGameover?.Invoke();
            objectReferenceManager.timescaleManager.PauseTimescale();
        }
    }

}
