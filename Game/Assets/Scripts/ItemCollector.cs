using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioSource winSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EndGoal"))
        {
            winSoundEffect.Play();
        }
    }
}