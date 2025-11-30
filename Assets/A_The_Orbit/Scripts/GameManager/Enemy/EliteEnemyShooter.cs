using UnityEngine;

public class EliteEnemyShooter : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Rotation")]
    public float rotationSpeed = 5f;
    //If the original sprite faces upwards, set -90
    //If the original sprite faces right, set 0
    //If the original sprite faces left, set 180
    public float angleOffset = -90f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    private float fireCooldown;

    private void Start()
    {
        //Help for finding Player at start if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    private void Update()
    {
        RotateTowardsPlayer();
        HandleShooting();
    }

    private void RotateTowardsPlayer()
    {
        if (player == null) return;

        //1. Look for the direction Vector from ourselves to the Player
        Vector3 direction = player.position - transform.position;

        //2. Calculate the angle (in Radians -> Degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //3. Create target Rotation (around Z axis) + Offset according to sprite facing
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + angleOffset);

        //4. Smoothly rotate towards the target (Slerp) for smoothness
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleShooting()
    {
        if (player == null || bulletPrefab == null || firePoint == null) return;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {// Shoot bullet using firePoint's rotation
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            fireCooldown = 1f / fireRate;
        }
    }
}