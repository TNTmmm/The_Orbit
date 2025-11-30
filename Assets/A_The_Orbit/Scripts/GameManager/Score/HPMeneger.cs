using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
public class HPMeneger : MonoBehaviour
{
    public static HPMeneger instance;

    public Text HPText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateHP();
    }

    public void UpdateHP()
    {
        if (Player.instance != null)
            HPText.text = Player.instance.currentHealth.ToString() + "  HP";
    }
}