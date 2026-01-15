using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameStateManager : MonoBehaviour
{
    private int playerHealth = 1000;
    private int currentScore;
    private int highScore;
    private bool isGameover;
    private bool isPaused;

    
    // Health funciton calls
    public void SetHealth(int health)
    {
        Mathf.Clamp(health, 0, 1000);
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

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void SetIsPaused(bool paused)
    {
        isPaused = paused;
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