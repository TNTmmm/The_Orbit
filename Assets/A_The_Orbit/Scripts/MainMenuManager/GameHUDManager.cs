using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHUDManager : MonoBehaviour
{
    public static GameHUDManager Instance;

    [Header("Main Visibility")]
    [Tooltip("Drag the HUD_Group (which contains health and weapon UI) here")]
    public GameObject hudRootPanel;
    public string gameplaySceneName = "GameScene";

    [Header("Weapon UI")]
    [Tooltip("Drag weapon display GameObjects (e.g., X_Shoot, Y_Shoot, Z_Shoot) here in order")]
    public GameObject[] weaponDisplays;

    [Header("Health UI (Single Image)")]
    [Tooltip("Drag HP_1, HP_2... (single Image each) here in order")]
    public Image[] hpIcons;

    [Header("Health Sprites")]
    [Tooltip("Picture Full Hp Sprite")]
    public Sprite fullHpSprite;

    [Tooltip("Picture Critical Hp Sprite")]
    public Sprite criticalHpSprite;

    [Tooltip("Picture Empty Hp Sprite")]
    public Sprite emptyHpSprite;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {

        //check if in gameplay scene to show/hide HUD
        if (hudRootPanel != null)
        {
            bool isGameplay = SceneManager.GetActiveScene().name == gameplaySceneName;

            // 
            // HUD Visibility Control
            if (!isGameplay)
            {
                hudRootPanel.SetActive(false);
            }
            // MainMenu or other scenes
        }
    }

    public void UpdateWeaponUI(int weaponIndex)
    {
        for (int i = 0; i < weaponDisplays.Length; i++)
        {
            if (weaponDisplays[i] != null)
            {
                weaponDisplays[i].SetActive(i == weaponIndex);
            }
        }
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        //1. Calculate health percentage
        float percentage = (float)currentHealth / maxHealth;

        //2. Choose the sprite to use (Normal or Critical)
        Sprite targetSprite = (percentage <= 0.6f) ? criticalHpSprite : fullHpSprite;

        //3. Loop through each HP icon
        for (int i = 0; i < hpIcons.Length; i++)
        {
            if (hpIcons[i] != null)
            {
                //If this health slot is still filled
                if (i < currentHealth)
                {
                    hpIcons[i].sprite = targetSprite;
                }
                //If this health slot is empty
                else
                {
                    hpIcons[i].sprite = emptyHpSprite;
                }
            }
        }
    }
}
