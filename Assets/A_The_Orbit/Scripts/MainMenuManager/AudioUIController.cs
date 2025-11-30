using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour
{
    [Header("Buttons")]
    public Button toggleBGMButton;
    public Button toggleSFXButton;

    private void Start()
    {
        if (toggleBGMButton != null)
            toggleBGMButton.onClick.AddListener(OnToggleBGM);

        if (toggleSFXButton != null)
            toggleSFXButton.onClick.AddListener(OnToggleSFX);
    }

    private void OnToggleBGM()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ToggleMusicBackground();
            AudioManager.instance.PlayClickSound();

        }
    }

    private void OnToggleSFX()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.ToggleSFX();
            AudioManager.instance.PlayClickSound();
        }
    }
}
