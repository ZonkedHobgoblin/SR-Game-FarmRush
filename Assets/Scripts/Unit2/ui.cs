using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ui : MonoBehaviour
{
    public int score;
    public int health;
    public GameObject gameovertext;
    public GameObject gameoverpanel;
    public TextMeshProUGUI healthtext;
    public TextMeshProUGUI scoretext;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        healthtext.text = "Health:" + health;
        scoretext.text = "Score:" + score;
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
