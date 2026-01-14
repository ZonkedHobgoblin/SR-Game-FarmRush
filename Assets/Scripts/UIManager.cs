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
        }
        else
        {
            Debug.LogWarning("UIManager: Could not subscribe to events because ObjectReferenceManager or GameBehaviourManager is missing.");
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
        // Get our ObjectReferenceManager
        objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();

        // Get and set the fill image (So we can change the colour if the cooldown is reached)
        sliderFillImage = Resources.Load("Images/SliderFillImage") as Image;

        // Button listener setup
        //Button btn = objectReferenceManager.uiRetryButton.GetComponent<Button>();
        //btn.onClick.AddListener(ReloadScene);
        //Button btn2 = objectReferenceManager.uiMenuButton.GetComponent<Button>();
        //btn2.onClick.AddListener(LoadMenuScene);
    }

    private void Start()
    {
        // Hide gameover stuff
        objectReferenceManager.uiGameoverObjectsParent.SetActive(false);
    }

    private void Update()
    {
        // Set defense cooldown text
        if (objectReferenceManager.playerManager.GetDefenseCooldown() && objectReferenceManager.uiDefenseCooldownText.text != "Defense ready")
        {
               objectReferenceManager.uiDefenseCooldownText.text = "Defense ready";
         }
        else if (!objectReferenceManager.playerManager.GetDefenseCooldown() && objectReferenceManager.uiDefenseCooldownText.text != "Defense unavailable")
        {
            objectReferenceManager.uiDefenseCooldownText.text = "Defense unavailable";
        }
        // Set cooldown slider value and colour
        objectReferenceManager.uiCooldownSlider.value = objectReferenceManager.playerManager.GetCooldown();
        if (objectReferenceManager.playerManager.GetIsPlayerCooldown() && objectReferenceManager.uiCooldownSlider.colors) {

        }
    }

    private void OnScore()
    {
        // Grab and the variables from out GameStateManager and update the UI elements
        // Set Score text
        objectReferenceManager.uiScoreText.text = objectReferenceManager.stateManager.GetScore().ToString();

        // Set high score text
        objectReferenceManager.uiHighScoreText.text = objectReferenceManager.stateManager.GetHighScore().ToString();
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


    }