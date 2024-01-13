using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    public PlayerManager playerManager;
    public ProgressManager progressManager;
    public TMP_InputField playerNameField;
    public GameObject logInMenu;
    public Button cancelSwitchPlayerButton;
    public TMP_Text greetingsText;

    void Start()
    {
        PlayersData playersData = playerManager.LoadFromJson();
        bool logInMenuIsActive = playersData.currentPlayer == null;
        logInMenu.SetActive(logInMenuIsActive);
        if (logInMenuIsActive)
        {
            cancelSwitchPlayerButton.interactable = false;
        }
        else
        {
            DisplayGreetings(playersData.currentPlayer);
        }
    }
    
    void DisplayGreetings(string nameToDisplay)
    {
        greetingsText.text = "Welcome back, " + nameToDisplay + "!";
    }

    public void LogInPlayer()
    {
        PlayersData playersData = playerManager.LoadFromJson();
        string inputName = playerNameField.text.ToString();
        if (string.IsNullOrEmpty(inputName))
        {
            playerNameField.GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f);
            return;
        }
        if (!playersData.players.Any(inputName.Equals))
        {
            playersData.players.Add(inputName);
        }
        playersData.currentPlayer = inputName;
        playerManager.SaveToJson(playersData);
        logInMenu.SetActive(false);
        cancelSwitchPlayerButton.interactable = true;
        playerNameField.GetComponent<Image>().color = Color.white;
        DisplayGreetings(inputName);
    }

    private bool CheckNameIsValid(string inputName)
    {
        return !string.IsNullOrEmpty(inputName);
    }

    public void SwitchPlayer()
    {
        logInMenu.SetActive(true);
    }

    public void CancelSwitchPlayer()
    {
        logInMenu.SetActive(false);
    }

    public void PlayGame()
    {
        string lastCompletedLevel = progressManager.GetLastCompletedLevel();
        if (lastCompletedLevel != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex  + (lastCompletedLevel[lastCompletedLevel.Length - 1] - '0') + 2);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
