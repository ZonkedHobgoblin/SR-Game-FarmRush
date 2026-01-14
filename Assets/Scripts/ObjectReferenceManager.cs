using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectReferenceManager : MonoBehaviour
{
    // Gets all objects and allows other scripts to call this script and retrieve them, stopping the constant finding of objects on every single script and (hopefully) improving performance

    #region Variables
    #region Scripts
    public PlayerController playerController;
    public AnimalSpawnManager animalSpawnManager;
    public UIManager uiManager;
    public SaveManager saveManager;
    public GameStateManager stateManager;
    public GameBehaviourManager gameBehaviourManager;
    public TimescaleManager timescaleManager;
    #endregion
    #region UI Elements
    public TextMeshProUGUI uiScoreText;
    public TextMeshProUGUI uiHighScoreText;
    public TextMeshProUGUI uiDefenseCooldownText;
    public TextMeshProUGUI uiGameoverText;
    public Slider uiHealthSlider;
    public Slider uiCooldownSlider;
    public Button uiRetryButton;
    public Button uiMenuButton;
    public Button uiResumeButton;
    public Button uiQuitButton;
    public Button uiPauseMenuButton;
    public GameObject uiGameoverObjectsParent;
    public GameObject uiPauseMenuObjectsParent;
    #endregion
    #endregion
    private void Awake()
    {
    // Get Script objects
        animalSpawnManager = GetComponent<AnimalSpawnManager>();
        saveManager = GetComponent<SaveManager>();
        stateManager = GetComponent<GameStateManager>();
        gameBehaviourManager = GetComponent<GameBehaviourManager>();
        timescaleManager = GetComponent<TimescaleManager>();
        playerController = FindFirstObjectByType<PlayerController>();
        uiManager = FindFirstObjectByType<UIManager>();


        // Get UI Elements
        uiScoreText = GameObject.Find("uiScoreText").GetComponent<TextMeshProUGUI>();
        uiHighScoreText = GameObject.Find("uiHighScoreText").GetComponent<TextMeshProUGUI>();
        uiDefenseCooldownText = GameObject.Find("uiDefenseCooldownText").GetComponent<TextMeshProUGUI>();
        uiGameoverText = GameObject.Find("uiGameoverText").GetComponent<TextMeshProUGUI>();
        uiHealthSlider = GameObject.Find("uiHealthSlider").GetComponent <Slider>();
        uiCooldownSlider = GameObject.Find("uiCooldownSlider").GetComponent<Slider>();
        uiRetryButton = GameObject.Find("uiRetryButton").GetComponent<Button>();
        uiMenuButton = GameObject.Find("uiMenuButton").GetComponent<Button>();
        uiResumeButton = GameObject.Find("uiResumeButton").GetComponent<Button>();
        uiQuitButton = GameObject.Find("uiQuitButton").GetComponent<Button>();
        uiPauseMenuButton = GameObject.Find("uiPauseMenuButton").GetComponent<Button>();
        uiGameoverObjectsParent = GameObject.Find("uiGameoverObjectsParent");
        uiPauseMenuObjectsParent = GameObject.Find("uiPauseMenuObjectsParent");
    }
}
