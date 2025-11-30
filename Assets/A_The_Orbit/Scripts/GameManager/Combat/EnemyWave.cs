using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Elite,
    Boss
}

public class EnemyWave : MonoBehaviour
{
    [Header("General Settings")]
    public EnemyType enemyType;
    public GameObject normalEnemyPrefab;
    public GameObject eliteEnemyPrefab;
    public GameObject bossEnemyPrefab;

    [Tooltip("Number of enemies to spawn")]
    public int count = 5;

    [Tooltip("Time interval between spawns")]
    public float timeBetween = 1f;

    [Tooltip("Path that enemies will follow")]
    public Transform[] pathPoints;

    [Tooltip("Movement speed of enemies")]
    public float speed = 3f;

    [Tooltip("Should enemy rotate to face its path direction?")]
    public bool rotationByPath = true;

    [Tooltip("If true, enemies will loop their path forever")]
    public bool loop = false;

    [Tooltip("Color used to draw path gizmo in Scene view")]
    public Color pathColor = Color.cyan;

    [Tooltip("Destroy this wave and its enemies after X seconds (0 = never destroy)")]
    public float destroyAfterSeconds = 0f;

    private void Start()
    {
        StartCoroutine(SpawnWave());

        // If destroyAfterSeconds is set, begin countdown to destroy this wave and its children
        if (destroyAfterSeconds > 0f)
        {
            StartCoroutine(AutoDestroyAfterSeconds(destroyAfterSeconds));
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = GetEnemyPrefab();
            if (prefab == null)
            {
                Debug.LogWarning("EnemyWave: Enemy prefab is not assigned.");
                yield break;
            }

            // Spawn enemy as child of this wave
            GameObject newEnemy = Instantiate(prefab, pathPoints[0].position, Quaternion.identity, transform);

            // Set path-following behavior
            FollowThePath follow = newEnemy.GetComponent<FollowThePath>();
            if (follow != null)
            {
                follow.SetPath(pathPoints, speed, rotationByPath, loop);
            }

            newEnemy.SetActive(true);
            yield return new WaitForSeconds(timeBetween);
        }

        // If not looping and destroyAfterSeconds is not used, destroy immediately
        if (!loop && destroyAfterSeconds <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AutoDestroyAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // This will destroy the path and all spawned enemies under it
    }

    private GameObject GetEnemyPrefab()
    {
        return enemyType switch
        {
            EnemyType.Normal => normalEnemyPrefab,
            EnemyType.Elite => eliteEnemyPrefab,
            EnemyType.Boss => bossEnemyPrefab,
            _ => null,
        };
    }

    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length < 2) return;

        Gizmos.color = pathColor;
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            if (pathPoints[i] != null && pathPoints[i + 1] != null)
                Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
        }

        if (loop && pathPoints.Length >= 2)
        {
            Gizmos.DrawLine(pathPoints[pathPoints.Length - 1].position, pathPoints[0].position);
        }
    }
}
