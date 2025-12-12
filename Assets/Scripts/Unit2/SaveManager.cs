using UnityEngine;
using System.IO;
using static SaveManager;

public class SaveManager : MonoBehaviour
{
    // Structure for json file
    private class Playerdata
    {
        public int Highscore;
    }

    // File path for save .json
    private string filePath = Application.persistentDataPath + "/playerData.json";

    // Save function
    public void SaveData(int score)
    {
        Playerdata data = new Playerdata();
        {
            data.Highscore = score;
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }
    }

    // Load function
    public int LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Playerdata player = JsonUtility.FromJson<Playerdata>(json);
            return player.Highscore;
        }
        else
        {
            return 0;
        }
    }
}