using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInspectScript : MonoBehaviour
{
    private Image _magnifyingGlassImage;

    private float currentAlpha = 0.0f;
    private float goalAlpha = 0.0f;

    void Start()
    {
        _magnifyingGlassImage = GetComponent<Image>();

        Color currentColor = _magnifyingGlassImage.color;
        currentColor.a = 0.0f;
        _magnifyingGlassImage.color = currentColor;
    }

    public void ChangeVisibility(float alpha)
        => goalAlpha = alpha;

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
        }
    }
}
