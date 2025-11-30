using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject[] bulletPrefabs;
    public float fireRate = 3.5f;
    public Transform firePoint;

    [Header("Multi-Shot Settings")]
    [Range(0, 2)] public int bulletLevel = 1;
    public float lateralOffset = 0.6f;
    public float angleOffset = 15f;

    [Header("Sound")]
    public AudioClip shootSFX; // ? ???????????

    private float nextFireTime = 0f;
    private int currentWeaponIndex = 0;
    private int[] bulletOptions = new int[] { 1, 3, 5 };

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        if (Input.GetKeyDown(KeyCode.Q)) SwitchWeapon(0);
        else if (Input.GetKeyDown(KeyCode.W)) SwitchWeapon(1);
        else if (Input.GetKeyDown(KeyCode.E)) SwitchWeapon(2);
    }

    void Shoot()
    {
        if (bulletPrefabs.Length == 0 || firePoint == null) return;

        int bulletCount = bulletOptions[bulletLevel];
        int middleIndex = bulletCount / 2;

        for (int i = 0; i < bulletCount; i++)
        {
            float xOffset = (i - middleIndex) * lateralOffset;
            float zAngle = -(i - middleIndex) * angleOffset;

            Vector3 spawnPos = firePoint.position + firePoint.right * xOffset;
            Quaternion spawnRot = firePoint.rotation * Quaternion.Euler(0f, 0f, zAngle);

            Instantiate(bulletPrefabs[currentWeaponIndex], spawnPos, spawnRot);
        }

        // Bullet VFX Fuckkkkkkkkkk Thissssssss Shitttttttt
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(shootSFX);
        }

    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < bulletPrefabs.Length)
        {
            currentWeaponIndex = index;

            if (GameHUDManager.Instance != null)
            {
                GameHUDManager.Instance.UpdateWeaponUI(currentWeaponIndex);
            }
            else
            {
                Debug.LogWarning("Can't find GameHUDManager!");
            }
            // ---------------------------------------------
        }
    }
    // ------------------------------------------------
    private void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }
}
