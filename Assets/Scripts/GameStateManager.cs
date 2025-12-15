using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameStateManager : MonoBehaviour
{
    private int playerHealth;
    private int currentScore;
    private int highScore;
    private bool isGameover;

    
    // Health funciton calls
    public void SetHealth(int health)
    {
        Mathf.Clamp(health, 0, 10);
        playerHealth = health;
        Debug.Log($"Health: {health}");
    }
    public int GetHealth()
    {
        return playerHealth;
    }

    // Score function calls
    public void SetScore(int score)
    {
        currentScore = score;
    }

    public int GetScore()
    {
        return currentScore;
    }


    // Gameover function calls
    public bool GetIsGameover()
    {
        return isGameover;
    }

    public void SetGameover(bool gameovervalue)
    {
        isGameover = gameovervalue;
    }

    // Highscore function calls
    public void SetHighScore(int score)
    {
        highScore = score;
    }

    public int GetHighScore()
    {
        return highScore;
    }


}