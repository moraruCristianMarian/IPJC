using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public GameObject endLevelPanel;

    void Start()
    {
        endLevelPanel.SetActive(false);
    }

    public void DisplayPanel(string bestLevelTime, string levelTime)
    {
        Time.timeScale = 0f;
        if (bestLevelTime == null)
        {
            scoreText.text = string.Format(
                "Current time: {0}\nBest time: {1}",
                levelTime,
                levelTime
            );
        }
        else if (String.Compare(bestLevelTime, levelTime) < 0)
            scoreText.text = string.Format(
                "Current time: {0}\nBest time: {1}",
                levelTime,
                bestLevelTime
            );
        else
            scoreText.text = string.Format(
                "Congrats! You beat your best time.\nCurrent time: {0}\nBest time: {1}",
                levelTime,
                bestLevelTime
            );
        endLevelPanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
