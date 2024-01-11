using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public TMP_InputField playerNameField;
    private string filePath = Application.dataPath + "/Data/PlayersDataFile.json";

    public void SaveToJson()
    {
        Debug.Log(filePath);
        PlayersData playersList = LoadFromJson();
        playersList.Players.Add(playerNameField.text.ToString());
        string jsonString = JsonUtility.ToJson(playersList, true);
        File.WriteAllText(this.filePath, jsonString);
    }

    public PlayersData LoadFromJson()
    {
        if (File.Exists(this.filePath))
        {
            string json = File.ReadAllText(this.filePath);
            PlayersData playersList = JsonUtility.FromJson<PlayersData>(json);
            return playersList;
        }
        else
        {
            return new PlayersData();
        }
    }
}
