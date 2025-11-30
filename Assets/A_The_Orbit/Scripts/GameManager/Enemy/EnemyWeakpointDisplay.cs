using UnityEngine;

public class EnemyWeakpointDisplay : MonoBehaviour
{
    [Header("Position Settings")]
    public Vector3 offset = new Vector3(2f, 0f, 0f);

    [Header("Weakpoint Prefabs")]
    public GameObject prefabX;
    public GameObject prefabY;
    public GameObject prefabZ;

    private Enemy enemy;
    private GameObject currentObject;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {

        UpdateWeakpointDisplay();
    }

    private void Update()
    {
        if (currentObject != null)
        {
            currentObject.transform.localPosition = offset;
            if (Camera.main != null)
                currentObject.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void UpdateWeakpointDisplay()
    {

        if (enemy == null) enemy = GetComponent<Enemy>();
        if (enemy == null) return;

        string wp = enemy.GetWeakPoint(); 
        GameObject prefabToSpawn = null;

        switch (wp)
        {
            case "X": prefabToSpawn = prefabX; break;
            case "Y": prefabToSpawn = prefabY; break;
            case "Z": prefabToSpawn = prefabZ; break;
        }

        if (prefabToSpawn != null)
        {

            if (currentObject != null) Destroy(currentObject);


            currentObject = Instantiate(prefabToSpawn, transform);
            currentObject.transform.localPosition = offset;
        }
        else
        {;
            Debug.LogError($"[EnemyWeakpointDisplay] Bug in EnemyWeakpointDisplay on {name}: Weakpoint '{wp}' has no assigned prefab in the Inspector!");
        }
    }
}
