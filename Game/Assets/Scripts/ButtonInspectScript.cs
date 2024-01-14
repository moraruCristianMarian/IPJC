using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ButtonInspectScript : MonoBehaviour
{
    private Image _magnifyingGlassImage;
    private TextMeshProUGUI _delayText;

    private float currentAlpha = 0.0f;
    private float goalAlpha = 0.0f;

    void Start()
    {
        _magnifyingGlassImage = GetComponent<Image>();
        _delayText = _magnifyingGlassImage.GetComponentInChildren<TextMeshProUGUI>();

        Color currentColor = _magnifyingGlassImage.color;
        currentColor.a = 0.0f;
        _magnifyingGlassImage.color = currentColor;

        currentColor = _delayText.color;
        currentColor.a = 0.0f;
        _delayText.color = currentColor;
    }

    public void ChangeVisibility(float alpha, float delay = 0.0f)
    {
        goalAlpha = alpha;

        delay = Mathf.Round(delay * 100.0f) / 100.0f;
        string newText = "Deactivation delay: " + delay + "s";
        _delayText.text = newText;
    }

    void FixedUpdate()
    {
        if (currentAlpha != goalAlpha)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, goalAlpha, 0.1f);
            if (Mathf.Abs(currentAlpha - goalAlpha) <= 0.005f)
                currentAlpha = goalAlpha;

            Color currentColor = _magnifyingGlassImage.color;
            currentColor.a = currentAlpha;
            _magnifyingGlassImage.color = currentColor;

            currentColor = _delayText.color;
            currentColor.a = currentAlpha;
            _delayText.color = currentColor;
        }
    }
}
