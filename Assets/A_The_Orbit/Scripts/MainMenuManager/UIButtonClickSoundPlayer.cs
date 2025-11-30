using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIButtonClickSoundPlayer : MonoBehaviour
{
    [SerializeField] private List<Button> buttonsToRegister = new List<Button>();
    [SerializeField] private AudioClip clickSFX;

    private void Awake()
    {
        foreach (var btn in buttonsToRegister)
        {
            if (btn != null)
                btn.onClick.AddListener(() => PlayClickSound());
        }
    }

    public void PlayClickSound()
    {
        if (AudioManager.instance != null && AudioManager.instance.IsSFXEnabled)
        {
            AudioManager.instance.PlaySFX(clickSFX);
        }
    }
}
