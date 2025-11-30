using UnityEngine;

public class DirectMoving : MonoBehaviour
{
    [Tooltip("Speed of the projectile on Y axis")]
    public float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
