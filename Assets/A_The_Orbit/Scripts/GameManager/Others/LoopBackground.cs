using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] private float tileHeight = 10f;

    private void Update()
    {
        if (transform.position.y < -tileHeight)
        {
            MoveToTop();
        }
    }

    private void MoveToTop()
    {
        Vector3 offset = new Vector3(0f, tileHeight * 2f, 0f);
        transform.position += offset;
    }
}
