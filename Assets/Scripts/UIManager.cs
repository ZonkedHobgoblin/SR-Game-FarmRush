using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Slider fill image
    public Image sliderFillImage;

    // Our Object reference manager
    private ObjectReferenceManager objectReferenceManager;
    private void OnEnable()
    {
        // Subscribe to the OnGameover action from our game behaviour manager
        objectReferenceManager.gameBehaviourManager.OnGameover += Gameover;
    }

    private void OnDisable()
    {
        // Destroy our subscription from the game behaviour manager to stop memory leaks (apparently?)
        objectReferenceManager.gameBehaviourManager.OnGameover -= Gameover;
    }

    private void Awake()
    {
        // Set the fill image (So we can change the colour if the cooldown is reached)
        sliderFillImage = objectReferenceManager.uiCooldownSlider.fillRect.GetComponent<Image>();

        // Button listener setup
        Button btn = objectReferenceManager.uiRetryButton.GetComponent<Button>();
        //btn.onClick.AddListener(ReloadScene);
        Button btn2 = objectReferenceManager.uiMenuButton.GetComponent<Button>();
        //btn2.onClick.AddListener(LoadMenuScene);
    }

    private void Update()
    {
        // Grab and the variables from out GameStateManager and update the UI elements
        // Set Score text
        objectReferenceManager.uiScoreText.text = objectReferenceManager.stateManager.GetScore().ToString();

        // Set high score text
        objectReferenceManager.uiHighScoreText.text = objectReferenceManager.stateManager.GetHighScore().ToString();

        // Set defense cooldown text
        if (objectReferenceManager.playerManager.GetDefenseCooldown())
        {
               objectReferenceManager.uiDefenseCooldownText.text = "Defense ready";
         }
        else
        {
            objectReferenceManager.uiDefenseCooldownText.text = "Defense unavailable";
        }

        // Set health slider
        objectReferenceManager.uiHealthSlider.value = objectReferenceManager.stateManager.GetHealth() / 10.0f;

        // Set cooldown slider
        objectReferenceManager.uiCooldownSlider.value = objectReferenceManager.playerManager.GetCooldown();
    }
    void Gameover()
    {
        objectReferenceManager.uiGameoverObjectsParent.SetActive(true);
        objectReferenceManager.uiGameoverText.text = $"GAME OVER\n\nScore: {objectReferenceManager.stateManager.GetScore().ToString()}\nHigh Score: {objectReferenceManager.stateManager.GetHighScore().ToString()}";
    }


    }