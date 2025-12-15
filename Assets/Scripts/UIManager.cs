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
        // Subscribe to the OnGameover action from our game behaviour manager
        objectReferenceManager.gameBehaviourManager.OnGameover += Gameover;
        objectReferenceManager.gameBehaviourManager.OnDamage += OnDamage;
        objectReferenceManager.gameBehaviourManager.OnScore += OnScore;
    }

    private void OnDisable()
    {
        // Destroy our subscription from the game behaviour manager to stop memory leaks (apparently?)
        objectReferenceManager.gameBehaviourManager.OnGameover -= Gameover;
        objectReferenceManager.gameBehaviourManager.OnDamage -= OnDamage;
        objectReferenceManager.gameBehaviourManager.OnScore -= OnScore;
    }

    private void Awake()
    {
        // Get our ObjectReferenceManager
        objectReferenceManager = FindFirstObjectByType<ObjectReferenceManager>();

        // Get and set the fill image (So we can change the colour if the cooldown is reached)
        sliderFillImage = Resources.Load("Images/SliderFillImage") as Image;

        // Button listener setup
        Button btn = objectReferenceManager.uiRetryButton.GetComponent<Button>();
        //btn.onClick.AddListener(ReloadScene);
        Button btn2 = objectReferenceManager.uiMenuButton.GetComponent<Button>();
        //btn2.onClick.AddListener(LoadMenuScene);
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
        // Set cooldown slider
        objectReferenceManager.uiCooldownSlider.value = objectReferenceManager.playerManager.GetCooldown();
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

    void Gameover()
    {
        objectReferenceManager.uiGameoverObjectsParent.SetActive(true);
        objectReferenceManager.uiGameoverText.text = $"GAME OVER\n\nScore: {objectReferenceManager.stateManager.GetScore().ToString()}\nHigh Score: {objectReferenceManager.stateManager.GetHighScore().ToString()}";
    }


    }