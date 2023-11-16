using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField]
    private Text powerUpText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            powerUpText.text = "PowerUp Activated!";
        }
    }
}