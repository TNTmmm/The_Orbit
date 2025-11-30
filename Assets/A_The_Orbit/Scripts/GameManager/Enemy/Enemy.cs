using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private string weakPoint = "X";

    [Header("Flash Settings")]
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashInterval = 0.05f;
    [SerializeField] private int flashCount = 3;
    [SerializeField] private MeshRenderer meshRenderer;

    private Material enemyMaterial;
    private Color originalColor1;
    private Color originalColor2;
    private static readonly string shaderColor1 = "_AlbedoColor1";
    private static readonly string shaderColor2 = "_AlbedoColor2";

    private void Awake()
    {
        currentHealth = maxHealth;

        if (meshRenderer != null)
        {
            enemyMaterial = meshRenderer.material;
            originalColor1 = enemyMaterial.GetColor(shaderColor1);
            originalColor2 = enemyMaterial.GetColor(shaderColor2);
        }
    }

    public void GetHit(int damage, string weaponType)
    {
        int actualDamage = (weaponType == weakPoint) ? damage * 2 : damage;
        currentHealth -= actualDamage;

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoint();
        }
        // ----------------------------------------

        Debug.Log($"Hit by {weaponType} for {actualDamage} damage!");

        // Trigger flash effect if material is valid
        if (enemyMaterial != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashOnHit());
        }

        if (currentHealth <= 0)
            Die();
    }

    private System.Collections.IEnumerator FlashOnHit()
    {
        for (int i = 0; i < flashCount; i++)
        {
            enemyMaterial.SetColor(shaderColor1, flashColor);
            enemyMaterial.SetColor(shaderColor2, flashColor);
            yield return new WaitForSeconds(flashInterval);

            enemyMaterial.SetColor(shaderColor1, originalColor1);
            enemyMaterial.SetColor(shaderColor2, originalColor2);
            yield return new WaitForSeconds(flashInterval);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    /// 1. Function to get the weak point (used by other scripts)
    public string GetWeakPoint()
    {
        return weakPoint;
    }

    //2. Function to change weak point (used by Boss) and update Prefab immediately
    public void SetWeakPoint(string newWeakPoint)
    {
        weakPoint = newWeakPoint;
        Debug.Log($"Enemy Weakness changed to: {newWeakPoint}");

        // Update the display prefab
        EnemyWeakpointDisplay display = GetComponent<EnemyWeakpointDisplay>();
        if (display != null)
        {
            display.UpdateWeakpointDisplay();
        }
    }
}
