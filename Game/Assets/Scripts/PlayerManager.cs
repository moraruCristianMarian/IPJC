using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public TMP_InputField playerNameField;
    public GameObject logInMenu;
    private string filePath = Application.dataPath + "/Data/PlayersDataFile.json";

    public TMP_Text greetingsText;

    void Start()
    {
        PlayersData playersData = LoadFromJson();
        bool logInMenuIsActive = playersData.currentPlayer == null;
        logInMenu.SetActive(logInMenuIsActive);
        if(!logInMenuIsActive) {
            DisplayGreetings(playersData.currentPlayer);
        }
    }

    public void LogInPlayer()
    {
        PlayersData playersData = LoadFromJson();
        string inputName = playerNameField.text.ToString();
        if (!playersData.players.Any(inputName.Contains))
        {
            playersData.players.Add(inputName);
        }
        playersData.currentPlayer = inputName;
        SaveToJson(playersData);
        logInMenu.SetActive(false);
        DisplayGreetings(inputName);
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

    void DisplayGreetings(string nameToDisplay)
    {
        greetingsText.text = "Welcome back, " + nameToDisplay + "!";
    }
}
