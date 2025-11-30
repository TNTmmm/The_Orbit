using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Boss Settings")]
    [SerializeField] private float phaseDuration = 5f; //Changed to 5f
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectilePrefabs; //Ammo prefabs for 3 patterns

    private Enemy enemyStats;
    private float phaseTimer;
    private int currentPhaseIndex = 0;

    //Loop of Weak Points: X -> Y -> Z
    private readonly string[] weakPointCycle = { "X", "Y", "Z" };

    private void Start()
    {
        enemyStats = GetComponent<Enemy>();
        phaseTimer = phaseDuration;

        //Start with phase 0
        ChangePhase(0);
    }

    private void Update()
    {
        //Countdown to next phase [cite: 45]
        phaseTimer -= Time.deltaTime;

        if (phaseTimer <= 0)
        {
            NextPhase();
            phaseTimer = phaseDuration;
        }

        //Run attack logic based on current phase
        ExecutePhasePattern();
    }

    private void NextPhase()
    {
        currentPhaseIndex++;
        if (currentPhaseIndex >= weakPointCycle.Length)
        {
            currentPhaseIndex = 0; // Loop back to first phase
        }

        ChangePhase(currentPhaseIndex);
    }

    private void ChangePhase(int index)
    {
        //1. Change Weakness [cite: 12]
        string newWeakness = weakPointCycle[index];
        enemyStats.SetWeakPoint(newWeakness);

        Debug.Log($"Boss Phase {index + 1}: Weakness is {newWeakness}");

        //(Optional) Here you might add effects or change the boss color to indicate weakness change
    }

    private void ExecutePhasePattern()
    {
        //Manage attack patterns based on phase[cite_end]
        switch (currentPhaseIndex)
        {
            case 0:
                PatternOne(); //Skill for weak point X
                break;
            case 1:
                PatternTwo(); //Skill for weak point Y
                break;
            case 2:
                PatternThree(); //Skill for weak point Z
                break;
        }
    }

    //--- End Example Attack Patterns ---

    //Variable to manage firing rate for boss
    private float fireTimer = 0f;
    [SerializeField] private float bossFireRate = 0.5f;

    private void PatternOne()
    {
        // 180 degrees to flip the projectile downwards
        if (Time.time >= fireTimer)
        {
            //shoot 3 projectiles spread out (based on downward 180)
            SpawnProjectile(Quaternion.Euler(0, 0, 180 - 15));
            SpawnProjectile(Quaternion.Euler(0, 0, 180));  //center
            SpawnProjectile(Quaternion.Euler(0, 0, 180 + 15));

            fireTimer = Time.time + bossFireRate;
        }
    }

    private void PatternTwo()
    {
        //180 degrees to flip the projectile downwards
        if (Time.time >= fireTimer)
        {
            float angle = Mathf.Sin(Time.time * 5) * 45f;
            // 180 + angle to make it sway at the bottom
            SpawnProjectile(Quaternion.Euler(0, 0, 180 + angle));

            fireTimer = Time.time + (bossFireRate * 0.5f);
        }
    }

    private void PatternThree()
    {
        if (Time.time >= fireTimer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            //In case player is missing
            if (player == null)
            {
                Debug.LogError("Player not found for Boss Pattern Three!");
            }
            else
            {
                //Found Player, aim at them
                Vector3 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

                SpawnProjectile(rotation);
            }
            // -----------------------

            fireTimer = Time.time + bossFireRate;
        }
    }

    private void SpawnProjectile(Quaternion rotation)
    {
        if (projectilePrefabs != null && projectilePrefabs.Length > 0)
        {
            // Spawn projectile (here we use the first one)
            Instantiate(projectilePrefabs[0], firePoint.position, rotation);
        }
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.TriggerVictory();
        }
    }

}
