using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ui : MonoBehaviour
{
    public int score;
    public float health;
    public GameObject gameovertext;
    public GameObject gameoverpanel;
    public Slider healthslider;
    public TextMeshProUGUI scoretext;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        healthslider.value = health / 10.0f;
        scoretext.text = "Score:\n" + score;
        if (health <= 0)
        {
            Gameover();
        }
    }
    void Gameover()
    {
        gameovertext.SetActive(true);
        gameoverpanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
