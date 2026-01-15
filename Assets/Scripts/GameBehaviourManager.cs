using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehaviourManager : MonoBehaviour
{
    public event Action OnGameover;
    public event Action OnDamage;
    public event Action OnScore;
    public event Action OnPause;


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

        // Allow our player to fire
        objectReferenceManager.playerController.SetCanPlayerFire(true);
    }

    public void IncrementHealth(int health)
    {
        objectReferenceManager.audioManager.PlayDamageSound();
        objectReferenceManager.stateManager.SetHealth(objectReferenceManager.stateManager.GetHealth() + health);
        OnDamage?.Invoke();
        CheckGameover();

    }

    public void IncrementScore(int score)
    {
        objectReferenceManager.stateManager.SetScore(objectReferenceManager.stateManager.GetScore() + score);
        UpdateHighScore();
        OnScore?.Invoke();
        
    }

    public void ReloadScene()
    {
        objectReferenceManager.timescaleManager.UnpauseTimescale();
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Single);
    }

    public void LoadMenuScene()
    {
        objectReferenceManager.timescaleManager.UnpauseTimescale();
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }

    public void TogglePauseMenu()
    {
        Debug.Log("Pause toggled");
        objectReferenceManager.stateManager.SetIsPaused(!objectReferenceManager.stateManager.GetIsPaused());
        objectReferenceManager.audioManager.ToggleMusic();
        if (objectReferenceManager.stateManager.GetIsPaused())
        {
            objectReferenceManager.playerController.SetCanPlayerFire(false);
            objectReferenceManager.timescaleManager.PauseTimescale();
            OnPause?.Invoke();

        }

        else
        {
            objectReferenceManager.timescaleManager.PauseTimescale();
            objectReferenceManager.timescaleManager.UnpauseTimescale();
            OnPause?.Invoke();
            objectReferenceManager.playerController.SetCanPlayerFire(true);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
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
            objectReferenceManager.audioManager.PlayGameoverSound();
            objectReferenceManager.stateManager.SetGameover(true);
            OnGameover?.Invoke();
            objectReferenceManager.timescaleManager.PauseTimescale();
            objectReferenceManager.playerController.enabled = false;
            objectReferenceManager.stateManager.enabled = false;
            objectReferenceManager.saveManager.enabled = false;
            objectReferenceManager.animalSpawnManager.enabled = false;
            objectReferenceManager.uiManager.enabled = false;
        }
    }
}
