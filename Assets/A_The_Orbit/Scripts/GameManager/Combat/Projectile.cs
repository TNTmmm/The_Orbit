using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 1;
    public string weaponType = "X"; // "X", "Y", or "Z"
    public bool enemyBullet = false;
    public bool destroyedByCollision = true;

    private void OnTriggerEnter(Collider collision)
    {
        // Player's bullet hits enemy
        if (!enemyBullet && collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetHit(damage, weaponType);
            }

            if (destroyedByCollision)
                Destroy(gameObject);
        }

        // Enemy's bullet hits player
        else if (enemyBullet && collision.CompareTag("Player"))
        {
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            if (destroyedByCollision)
                Destroy(gameObject);
        }
    }
}
