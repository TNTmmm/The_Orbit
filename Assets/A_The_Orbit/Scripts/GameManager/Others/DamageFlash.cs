using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashInterval = 0.05f;
    [SerializeField] private int flashCount = 3;

    [Header("Visuals")]
    [SerializeField] private MeshRenderer meshRenderer;
    private Material flashMaterial;

    private static readonly string shaderColor1 = "_AlbedoColor1";
    private static readonly string shaderColor2 = "_AlbedoColor2";

    private Color originalColor1;
    private Color originalColor2;

    private void Awake()
    {
        if (meshRenderer != null)
        {
            flashMaterial = meshRenderer.material;
            originalColor1 = flashMaterial.GetColor(shaderColor1);
            originalColor2 = flashMaterial.GetColor(shaderColor2);
        }
    }

    public void TriggerFlash()
    {
        if (flashMaterial != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashCoroutine());
        }
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            flashMaterial.SetColor(shaderColor1, flashColor);
            flashMaterial.SetColor(shaderColor2, flashColor);
            yield return new WaitForSeconds(flashInterval);

            flashMaterial.SetColor(shaderColor1, originalColor1);
            flashMaterial.SetColor(shaderColor2, originalColor2);
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
