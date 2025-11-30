using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    private Transform[] path;
    private float speed;
    private bool rotationByPath;
    private bool loop;

    private int currentPointIndex = 0;
    private bool hasPath = false;

    public void SetPath(Transform[] pathPoints, float moveSpeed, bool rotate, bool isLoop)
    {
        if (pathPoints == null || pathPoints.Length == 0)
        {
            Debug.LogWarning($"[FollowThePath] No path received for {gameObject.name}");
            return;
        }

        path = pathPoints;
        speed = moveSpeed;
        rotationByPath = rotate;
        loop = isLoop;

        transform.position = path[0].position;
        currentPointIndex = 0;
        hasPath = true;

        Debug.Log($"[FollowThePath] {gameObject.name} started following path.");
    }

    private void Update()
    {
        if (!hasPath || path == null || path.Length == 0) return;

        Transform targetPoint = path[currentPointIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (rotationByPath && direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex++;

            if (currentPointIndex >= path.Length)
            {
                if (loop)
                {
                    currentPointIndex = 0;
                }
                else
                {
                    hasPath = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
