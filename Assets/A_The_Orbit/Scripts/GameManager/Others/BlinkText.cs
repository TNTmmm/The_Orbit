using TMPro;
using UnityEngine;

public class BlinkSmoothText : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;
    public float blinkSpeed = 1.0f; 

    public Color startColor = Color.white;
    public Color endColor = Color.cyan;

    void Update()
    {
        if (textToBlink == null) return;

        float lerp = Mathf.PingPong(Time.unscaledTime * blinkSpeed, 1f);

        Color newColor = Color.Lerp(startColor, endColor, lerp);
        textToBlink.color = newColor;
    }
}
