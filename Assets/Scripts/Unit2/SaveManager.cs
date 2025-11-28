using UnityEngine;
using System.IO;
using static SaveManager;

public class SaveManager : MonoBehaviour
{
    public ui PlayerUI;
    public class Playerdata
    {
        public int Highscore;
    }
    public int LoadedScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        PlayerUI.HighScoreUI = LoadedScore;
    }

    void Update()
    {
        if (PlayerUI.score > LoadedScore)
        {
            LoadedScore = PlayerUI.score;
            PlayerUI.HighScoreUI = LoadedScore;
            SaveData();
        }
    }

    void SaveData()
    {
        Playerdata data = new Playerdata();
        {
            data.Highscore = LoadedScore;
            string json = JsonUtility.ToJson(data, true);
            string path = Application.persistentDataPath + "/playerData.json";
            File.WriteAllText(path, json);
        }
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Playerdata player = JsonUtility.FromJson<Playerdata>(json);
            LoadedScore = player.Highscore;
        }
    }
}