using System.Collections;
using UnityEngine;

[System.Serializable]
public class WaveSchedule
{
    [Tooltip("Time to spawn wave after game started")]
    public float delay;

    [Tooltip("Prefab of EnemyWave")]
    public GameObject wavePrefab;

    [Tooltip("Start from this wave onward")]
    public bool startHere = false;
}

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public WaveSchedule[] waves;

    private void Start()
    {
        int startIndex = 0;

        // search wave to startHere
        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].startHere)
            {
                startIndex = i;
                break;
            }
        }

        // start spawn at startIndex
        for (int i = startIndex; i < waves.Length; i++)
        {
            StartCoroutine(SpawnWaveWithDelay(waves[i].delay, waves[i].wavePrefab));
        }
    }

    private IEnumerator SpawnWaveWithDelay(float delay, GameObject wavePrefab)
    {
        if (delay > 0f)
            yield return new WaitForSeconds(delay);

        if (wavePrefab != null)
        {
            Instantiate(wavePrefab);
        }
        else
        {
            Debug.LogWarning("WaveManager: wavePrefab is null!");
        }
    }
}
