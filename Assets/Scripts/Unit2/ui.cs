using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Experimental.GraphView;

public class ui : MonoBehaviour
{
    public int score;
    public float health;
    public GameObject gameovertext;
    public GameObject gameoverpanel;
    public Slider healthslider;
    public Slider cooldownslider;
    public TextMeshProUGUI scoretext;
    public pc2 PlayerCont2;
    private Image fillImage;
    public int HighScoreUI;
    public TextMeshProUGUI highscoretext;
    // Start is called before the first frame update
    void Start()
    {
        fillImage = cooldownslider.fillRect.GetComponent<Image>();
    }
    private void Update()
    {
        healthslider.value = health / 10.0f;
        cooldownslider.value = PlayerCont2.cooldown;
        scoretext.text = "Score:\n" + score;
        highscoretext.text = "High Score:\n" + HighScoreUI;
        if (health <= 0)
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

    }
    void Gameover()
    {
        gameovertext.SetActive(true);
        gameoverpanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
