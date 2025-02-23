using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    public PlayerManager playerManager;

    public string FilePath
    {
        get
        {
            return Application.dataPath
                + playerManager.CurrentPlayer
                + "ProgressData.json";
        }
    }

    public void SaveToJson(string levelName, string levelTime)
    {
        PlayerProgressData playerProgressData = LoadFromJson();
        playerProgressData.lastCompletedLevel =
            String.Compare(levelName, "Level4") == 0 ? "Tutorial0" : levelName;
        LevelData levelData = playerProgressData
            .bestTimesPerLevel.Where(item => item.name == levelName)
            .FirstOrDefault();
        if (levelData != null)
        {
            if (String.Compare(levelData.bestTime, levelTime) > 0)
                playerProgressData.bestTimesPerLevel.ForEach(item =>
                {
                    if (item.name == levelName)
                    {
                        item.bestTime = levelTime;
                    }
                });
        }
        else
        {
            playerProgressData.bestTimesPerLevel.Add(
                new LevelData { name = levelName, bestTime = levelTime }
            );
        }
        string jsonString = JsonUtility.ToJson(playerProgressData, true);
        File.WriteAllText(this.FilePath, jsonString);
    }

    public PlayerProgressData LoadFromJson()
    {
        if (File.Exists(this.FilePath))
        {
            string json = File.ReadAllText(this.FilePath);
            PlayerProgressData playerProgressData = JsonUtility.FromJson<PlayerProgressData>(json);
            return playerProgressData;
        }
        else
        {
            return new PlayerProgressData();
        }
    }

    public string GetBestTime(string levelName)
    {
        PlayerProgressData playerProgressData = LoadFromJson();
        LevelData levelData = playerProgressData
            .bestTimesPerLevel.Where(item => item.name == levelName)
            .FirstOrDefault();
        return levelData != null? levelData.bestTime : null;
    }

    public string GetLastCompletedLevel()
    {
        PlayerProgressData playerProgressData = LoadFromJson();
        return playerProgressData.lastCompletedLevel;
    }
}
