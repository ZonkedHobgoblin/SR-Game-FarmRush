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
            // Try finding it again just in case Awake failed or hasn't run
            objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();
        }

        // Check to see if we have the dependency
        if (objectReferenceManager != null && objectReferenceManager.gameBehaviourManager != null)
        {
            objectReferenceManager.gameBehaviourManager.OnDamage += OnDamage;
            objectReferenceManager.gameBehaviourManager.OnScore += OnScore;
            objectReferenceManager.gameBehaviourManager.OnGameover += OnGameover;
            objectReferenceManager.gameBehaviourManager.TogglePauseMenu += TogglePauseMenu;
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
    if (objectReferenceManager.uiCooldownSlider != null)
        {
            sliderFillImage = objectReferenceManager.uiCooldownSlider.fillRect.GetComponent<Image>();
        }

    // Button listener setup
    Button btn = objectReferenceManager.uiRetryButton.GetComponent<Button>();
    btn.onClick.AddListener(objectReferenceManager.gameBehaviourManager.ReloadScene);
    Button btn2 = objectReferenceManager.uiMenuButton.GetComponent<Button>();
    btn2.onClick.AddListener(objectReferenceManager.gameBehaviourManager.LoadMenuScene);
}

    private void Start()
    {
        // Hide gameover and pause menu stuff
        objectReferenceManager.uiGameoverObjectsParent.SetActive(false);
        objectReferenceManager.uiPauseMenuObjectsParent.SetActive(false);

        // Update our score & high score
        OnScore();
    }

    private void Update()
    {
        // Set defense cooldown text
        if (!objectReferenceManager.playerManager.GetDefenseCooldown() && objectReferenceManager.uiDefenseCooldownText.text != "Defense ready")
        {
               objectReferenceManager.uiDefenseCooldownText.text = "Defense ready";
         }
        else if (objectReferenceManager.playerManager.GetDefenseCooldown() && objectReferenceManager.uiDefenseCooldownText.text != "Defense unavailable")
        {
            objectReferenceManager.uiDefenseCooldownText.text = "Defense unavailable";
        }
        // Set cooldown slider value and colour
        objectReferenceManager.uiCooldownSlider.value = objectReferenceManager.playerManager.GetCooldown();
        if (objectReferenceManager.playerManager.GetIsPlayerCooldown())
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
        }
        else
        {
            objectReferenceManager.uiPauseMenuObjectsParent.SetActive(false);
        }
    }

    }