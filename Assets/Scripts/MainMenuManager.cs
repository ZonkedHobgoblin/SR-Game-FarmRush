using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private Button uiPlayButton;
    private Button uiQuitButton;
    private TextMeshProUGUI uiHighScoreText;
    private SaveManager saveManager;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        // Get our button references (And save manager / text to display high score)
        uiPlayButton = GameObject.Find("uiPlayButton").GetComponent<Button>();
        uiQuitButton = GameObject.Find("uiQuitButton").GetComponent<Button>();
        uiHighScoreText = GameObject.Find("uiHighScoreText").GetComponent<TextMeshProUGUI>();
        saveManager = GetComponent<SaveManager>();
        audioManager = GetComponent<AudioManager>();

        // Button listener setup
        Button playButton = uiPlayButton.GetComponent<Button>();
        playButton.onClick.AddListener(audioManager.PlayUIClick);
        playButton.onClick.AddListener(LoadMainLevel);
        Button quitButton = uiQuitButton.GetComponent<Button>();
        quitButton.onClick.AddListener(audioManager.PlayUIClick);
        quitButton.onClick.AddListener(QuitGame);

        // Set our high score
        uiHighScoreText.text = "High Score:\n" + saveManager.LoadData().ToString();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void LoadMainLevel()
    {
        SceneManager.LoadScene("MainLevel", LoadSceneMode.Single);
    }
}
