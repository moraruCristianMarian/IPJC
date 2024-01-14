using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource winSoundEffect;
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private ProgressManager progressManager;
    [SerializeField] private Timer timer;
    [SerializeField] private EndLevelManager endLevelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndGoal"))
        {
            winSoundEffect.Play();
            // wait some seconds before next level
            Invoke("CompleteLevel", 0.5f);
        }

        if (collision.gameObject.HasCustomTag("GravityRotationPotion"))
        {
            Gravity gravityScript = FindObjectOfType<Gravity>();
            if (gravityScript != null)
            {
                gravityScript.CanRotateGravity = true;

                
                if (collision.gameObject.HasCustomTag("GlobalGravityRotationPotion"))
                    gravityScript.CanToggleGlobalGravity = true;
            }
            collectSoundEffect.Play();
        }
        else
        if (collision.gameObject.HasCustomTag("TimeTravelPotion"))
        {
            TimeTravel timeTravelScript = FindObjectOfType<TimeTravel>();
            if (timeTravelScript != null)
            {
                timeTravelScript.enabled = true;
            }
            collectSoundEffect.Play();
        }
    }

    private void CompleteLevel() {
        Scene scene = SceneManager.GetActiveScene();
        string levelName = scene.name;
        string levelTime = timer.timeText.text;
        string bestLevelTime = progressManager.GetBestTime(levelName);
        progressManager.SaveToJson(levelName, levelTime);
        endLevelManager.DisplayPanel(bestLevelTime, levelTime);
    }
}