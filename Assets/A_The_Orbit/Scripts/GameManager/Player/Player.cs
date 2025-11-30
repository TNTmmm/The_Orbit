using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] public int currentHealth;

    public static Player instance;

    [Header("Flash Settings")]
    [SerializeField] private Color flashColor = Color.white;     // The color used when flashing on hit
    [SerializeField] private float flashInterval = 0.05f;         // Time between flash on/off
    [SerializeField] private int flashCount = 3;                  // Number of flash cycles

    [Header("Visuals")]
    [SerializeField] private MeshRenderer meshRenderer;           // The renderer of the player mesh
    private Material playerMaterial;

    // Shader property names - these match your custom shader
    private static readonly string shaderColor1 = "_AlbedoColor1";
    private static readonly string shaderColor2 = "_AlbedoColor2";

    private Color originalColor1;
    private Color originalColor2;




    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Initialize health
        currentHealth = maxHealth;

        // Cache the player's material and store original shader colors
        if (meshRenderer != null)
        {
            playerMaterial = meshRenderer.material;
            originalColor1 = playerMaterial.GetColor(shaderColor1);
            originalColor2 = playerMaterial.GetColor(shaderColor2);
        }
    }

    // 1. Start method to reset health UI at game start
    private void Start()
    {
        
        UpdateAllUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        
        UpdateAllUI();

        if (playerMaterial != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashOnHit());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        
        UpdateAllUI();
        // ------------------------------------------
    }

    
    private void UpdateAllUI()
    {
        
        if (GameHUDManager.Instance != null)
        {
            GameHUDManager.Instance.UpdateHealth(currentHealth, maxHealth);
        }

        
        if (HPMeneger.instance != null)
        {
            HPMeneger.instance.UpdateHP();
        }
    }
    // -----------------------------------------------

    private void Die()
    {
        if (GameStateManager.Instance != null) GameStateManager.Instance.TriggerGameOver();
        gameObject.SetActive(false);
    }

    private System.Collections.IEnumerator FlashOnHit()
    {
        for (int i = 0; i < flashCount; i++)
        {
            playerMaterial.SetColor(shaderColor1, flashColor);
            playerMaterial.SetColor(shaderColor2, flashColor);
            yield return new WaitForSeconds(flashInterval);

            playerMaterial.SetColor(shaderColor1, originalColor1);
            playerMaterial.SetColor(shaderColor2, originalColor2);
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
