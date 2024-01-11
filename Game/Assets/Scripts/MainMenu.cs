using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public PlayerManager playerManager;

   public void PlayGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void Tutorial()
   {
      SceneManager.LoadScene("Tutorial");
   }

   public void SwitchPlayer()
   {
      playerManager.SwitchPlayer();
   }


   public void QuitGame()
   {
      Application.Quit();
   }
}
