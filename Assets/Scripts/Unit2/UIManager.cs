using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEditor.Experimental.GraphView;

public class ui : MonoBehaviour
{
    public int heyThere
    


    private GameBehaviourManager gameBehaviourManager;

    private void OnEnable()
    {
        gameBehaviourManager = FindFirstObjectByType<GameBehaviourManager>();
        // Subscribe to the OnGameover action from our game behaviour manager
        gameBehaviourManager.OnGameover += Gameover()
    }

    private void OnDisable()
    {
        // Destroy our subscription from the game behaviour manager to stop memory leaks (apparently?)
        gameBehaviourManager.OnGameover -= Gameover()
    }

    void Start()
    {
        // Get game objects/components
        // Get the game behaviour manager
        //gameBehaviourManager = FindFirstObjectByType<GameBehaviourManager>();
    }

    private void Gameover()
    {

    }













































    public TextMeshProUGUI gameovertext;
    public GameObject GameOverStuff;
    public GameObject gameoverpanel;
    public TextMeshProUGUI defensetext;
    public Slider healthslider;
    public Slider cooldownslider;
    public TextMeshProUGUI scoretext;
    public pc2 PlayerCont2;
    public int HighScoreUI;
    public TextMeshProUGUI highscoretext;
    public Button retryButton;
    public Button menuButton;


    private Image fillImage;
    private GameStateManager gameStateManager;
    // Start is called before the first frame update
    void Start()
    {
        fillImage = cooldownslider.fillRect.GetComponent<Image>();
        Button btn = retryButton.GetComponent<Button>();
        btn.onClick.AddListener(ReloadScene);
        Button btn2 = menuButton.GetComponent<Button>();
        btn2.onClick.AddListener(LoadMenuScene);
        gameStateManager = FindFirstObjectByType<GameStateManager>();

    }
    private void Update()
    {
        healthslider.value = health / 10.0f;
        cooldownslider.value = PlayerCont2.cooldown;
        scoretext.text = "Score:\n" + score;
        highscoretext.text = "High Score:\n" + HighScoreUI;
        if (gameStateManager.GetHealth() <= 0)
        {
            Gameover();
        }
        
        if ((PlayerCont2.cooldown == 1) && (fillImage.color!= Color.red))
        {
            fillImage.color = Color.red;
        }
        else if ((PlayerCont2.cooldown < 1) && (fillImage.color!= new Color(255, 255, 0)))
        {
            fillImage.color = new Color(255, 255, 0);
        }
        if (PlayerCont2.defenseCooldown)
        {
            defensetext.text = "Defense on cooldown";
        }
        else
        {
            defensetext.text = "Defense ready";
        }
        

    }
    void Gameover()
    {
        GameOverStuff.SetActive(true);
        gameoverpanel.SetActive(true);
        gameovertext.text = $"GAME OVER\n\nScore: {score}\nHigh Score: {HighScoreUI}";
        Time.timeScale = 0f;
    }

    void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype 2", LoadSceneMode.Single);
    }

    void LoadMenuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}
