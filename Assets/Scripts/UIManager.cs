using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Slider fill image
    private Image sliderFillImage;

    // Toggle bool for text
    private bool isDefenseCooldown;

    // Our Object reference manager
    private ObjectReferenceManager objectReferenceManager;
    private void OnEnable()
    {
        // Check to see if we have the manager
        if (objectReferenceManager == null)
        {
            // Try againcase awake hasn't run
            objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();
        }

        // Check to see if we have the dependency
        if (objectReferenceManager != null && objectReferenceManager.gameBehaviourManager != null)
        {
            objectReferenceManager.gameBehaviourManager.OnDamage += OnDamage;
            objectReferenceManager.gameBehaviourManager.OnScore += OnScore;
            objectReferenceManager.gameBehaviourManager.OnGameover += OnGameover;
            objectReferenceManager.gameBehaviourManager.OnPause += TogglePauseMenu;
        }
    }

    private void OnDisable()
    {
        // Destroy our subscription from the game behaviour manager to stop memory leaks (apparently?)
        // Only destroying if event manager still exists because it will throw an absolute fit if we try this while it doesn't exist
        if (objectReferenceManager != null && objectReferenceManager.gameBehaviourManager != null)
        {
            objectReferenceManager.gameBehaviourManager.OnGameover -= OnGameover;
            objectReferenceManager.gameBehaviourManager.OnDamage -= OnDamage;
            objectReferenceManager.gameBehaviourManager.OnScore -= OnScore;
        }
    }

    private void Awake()
    {
    // Get the ref Manager
    objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();

    // Get the slider fill Image
    sliderFillImage = objectReferenceManager.uiCooldownSlider.fillRect.GetComponent<Image>();

    // Button listener setup
    Button retryButton = objectReferenceManager.uiRetryButton.GetComponent<Button>();
    retryButton.onClick.AddListener(objectReferenceManager.gameBehaviourManager.ReloadScene);
    retryButton.onClick.AddListener(objectReferenceManager.audioManager.PlayUIClick);
    Button menuButton = objectReferenceManager.uiMenuButton.GetComponent<Button>();
    menuButton.onClick.AddListener(objectReferenceManager.gameBehaviourManager.LoadMenuScene);
    menuButton.onClick.AddListener(objectReferenceManager.audioManager.PlayUIClick);
    Button resumeButton = objectReferenceManager.uiResumeButton.GetComponent<Button>();
    resumeButton.onClick.AddListener(objectReferenceManager.gameBehaviourManager.TogglePauseMenu);
    resumeButton.onClick.AddListener(objectReferenceManager.audioManager.PlayUIClick);
    Button pauseMenuButton = objectReferenceManager.uiPauseMenuButton.GetComponent<Button>();
    pauseMenuButton.onClick.AddListener(objectReferenceManager.gameBehaviourManager.LoadMenuScene);
    pauseMenuButton.onClick.AddListener(objectReferenceManager.audioManager.PlayUIClick);
    Button quitButton = objectReferenceManager.uiQuitButton.GetComponent<Button>();
    quitButton.onClick.AddListener(objectReferenceManager.gameBehaviourManager.QuitGame);
    quitButton.onClick.AddListener(objectReferenceManager.audioManager.PlayUIClick);
    }

    private void Start()
    {
        // Hide the cursor
        Cursor.visible = false;

        // Hide gameover and pause menu stuff
        objectReferenceManager.uiGameoverObjectsParent.SetActive(false);
        objectReferenceManager.uiPauseMenuObjectsParent.SetActive(false);

        // Update our score & high score
        OnScore();
    }

    private void Update()
    {
        // Set defense cooldown text
        if (!objectReferenceManager.playerController.GetDefenseCooldown() && objectReferenceManager.uiDefenseCooldownText.text != "Defense ready")
        {
               objectReferenceManager.uiDefenseCooldownText.text = "Defense ready";
         }
        else if (objectReferenceManager.playerController.GetDefenseCooldown() && objectReferenceManager.uiDefenseCooldownText.text != "Defense unavailable")
        {
            objectReferenceManager.uiDefenseCooldownText.text = "Defense unavailable";
        }
        // Set cooldown slider value and colour
        objectReferenceManager.uiCooldownSlider.value = objectReferenceManager.playerController.GetCooldown();
        if (objectReferenceManager.playerController.GetIsPlayerCooldown())
        {
            sliderFillImage.color = Color.red;
        }
        else
        {
            sliderFillImage.color = Color.yellow;
        }

    }

    private void OnScore()
    {
        // Grab and the variables from out GameStateManager and update the UI elements
        // Set Score text
        objectReferenceManager.uiScoreText.text = ("Score:\n" + objectReferenceManager.stateManager.GetScore().ToString());

        // Set high score text
        objectReferenceManager.uiHighScoreText.text = ("High Score:\n" + objectReferenceManager.stateManager.GetHighScore().ToString());
    }

    private void OnDamage()
    {
        // Set health slider
        objectReferenceManager.uiHealthSlider.value = objectReferenceManager.stateManager.GetHealth() / 10.0f;
    }

    private void OnGameover()
    {
        objectReferenceManager.uiGameoverObjectsParent.SetActive(true);
        objectReferenceManager.uiGameoverText.text = $"GAME OVER\n\nScore: {objectReferenceManager.stateManager.GetScore().ToString()}\nHigh Score: {objectReferenceManager.stateManager.GetHighScore().ToString()}";
    }

    private void TogglePauseMenu()
    {
        if (objectReferenceManager.stateManager.GetIsPaused())
        {
            objectReferenceManager.uiPauseMenuObjectsParent.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
            objectReferenceManager.uiPauseMenuObjectsParent.SetActive(false);
        }
    }

    }