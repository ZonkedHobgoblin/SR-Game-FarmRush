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
    #region Variable defenitions
    #region Other/Assets
    // Other/Assets
    public Image sliderFillImage;
    #endregion

    #region System Managers
    // System managers
    private GameBehaviourManager gameBehaviourManager;
    private GameStateManager stateManager;
    private PlayerManager playerManager;
    #endregion

    #region Game Objects
    // Game Objects
    #region UI Elements
    // Main UI Elements
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI defenseCooldownText;
    private Slider healthSlider;
    private Slider cooldownSlider;
    private Button retryButton;
    private Button menuButton;
    #endregion
    #region Gameover elements
    // Gameover elements
    private GameObject gameoverObjectsParent;
    private TextMeshProUGUI gameoverText;
    #endregion
    #endregion
    #endregion

    private void OnEnable()
    {
        // Subscribe to the OnGameover action from our game behaviour manager
        gameBehaviourManager.OnGameover += Gameover;
    }

    private void OnDisable()
    {
        // Destroy our subscription from the game behaviour manager to stop memory leaks (apparently?)
        gameBehaviourManager.OnGameover -= Gameover;
    }

    private void Awake()
    {
        #region Get GameObjetcs
        // Get game objects/components
        #region Get Managers
        // Get the GameBehaviourManager
        gameBehaviourManager = FindFirstObjectByType<GameBehaviourManager>();
        // Get the GameStateManager
        stateManager = FindFirstObjectByType<GameStateManager>();
        // Get the PlayerManager
        playerManager = FindFirstObjectByType<PlayerManager>();
        #endregion
        #region Get UI Objects
        // Get UI objects
        // Get health slider
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        // Get cooldown slider
        cooldownSlider = GameObject.Find("CooldownSlider").GetComponent<Slider>();
        // Get score text
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        // Get high score text
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        // Get defense cooldown text
        defenseCooldownText = GameObject.Find("DefenseCooldownText").GetComponent<TextMeshProUGUI>();
        #region Get gameover objects
        // Get game over UI elements
        // Get parent of all game over UI objects for a simple active toggle
        gameoverObjectsParent = GameObject.Find("GameoverParent");
        // Get gameover text
        gameoverText = GameObject.Find("GameoverText").GetComponent<TextMeshProUGUI>();
        // Get Gameover UI Buttons
        // Get Retry Button
        retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        // Get Menu Button
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        #endregion
        #endregion
        #endregion

        // Set the fill image (So we can change the colour if the cooldown is reached)
        sliderFillImage = cooldownSlider.fillRect.GetComponent<Image>();

        // Button listener setup
        Button btn = retryButton.GetComponent<Button>();
        //btn.onClick.AddListener(ReloadScene);
        Button btn2 = menuButton.GetComponent<Button>();
        //btn2.onClick.AddListener(LoadMenuScene);
    }

    private void Update()
    {
        // Grab and the variables from out GameStateManager and update the UI elements
        // Set Score text
        scoreText.text = stateManager.GetScore().ToString();

        // Set high score text
        highScoreText.text = stateManager.GetHighScore().ToString();

        // Set defense cooldown text
        if (playerManager.GetDefenseCooldown())
        {
               defenseCooldownText.text = "Defense ready";
         }
        else
        {
            defenseCooldownText.text = "Defense unavailable";
        }

        // Set health slider
        healthSlider.value = stateManager.GetHealth() / 10.0f;

        // Set cooldown slider
        cooldownSlider.value = playerManager.GetCooldown();
    }
    void Gameover()
    {
        gameoverObjectsParent.SetActive(true);
        gameoverText.text = $"GAME OVER\n\nScore: {stateManager.GetScore().ToString()}\nHigh Score: {stateManager.GetHighScore().ToString()}";
    }


    }
    //void Gameover()
    //{
        //Time.timeScale = 0f;
    //}