using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string filePath = Application.dataPath + "/Data/PlayersDataFile.json";
    
    public string CurrentPlayer
    {
        get
        {
            PlayersData playersData = LoadFromJson();
            return playersData.currentPlayer;
        }
    }

    public void SaveToJson(PlayersData playersData)
    {
        string jsonString = JsonUtility.ToJson(playersData, true);
        File.WriteAllText(this.filePath, jsonString);
    }

    public PlayersData LoadFromJson()
    {
        if (File.Exists(this.filePath))
        {
            string json = File.ReadAllText(this.filePath);
            PlayersData playersData = JsonUtility.FromJson<PlayersData>(json);
            return playersData;
        }
        else
        {
            return new PlayersData();
        }
    }
}
