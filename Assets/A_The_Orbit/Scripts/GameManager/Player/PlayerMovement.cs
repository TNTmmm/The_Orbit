using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Borders movementBorders;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        CalculateBorders();
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButton(0))
        {
            MoveToPointer(Input.mousePosition);
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0)
        {
            MoveToPointer(Input.GetTouch(0).position);
        }
#endif
    }

    public void MoveToPointer(Vector3 pointerScreenPosition)
    {
        Vector3 target = mainCamera.ScreenToWorldPoint(pointerScreenPosition);
        target.z = transform.position.z;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        ClampPosition();
    }

    private void ClampPosition()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, movementBorders.MinX, movementBorders.MaxX),
            Mathf.Clamp(transform.position.y, movementBorders.MinY, movementBorders.MaxY),
            transform.position.z
        );
    }

    private void CalculateBorders()
    {
        Vector2 min = Vector2.zero;
        Vector2 max = Vector2.one;

        movementBorders.MinX = mainCamera.ViewportToWorldPoint(min).x + movementBorders.minXOffset;
        movementBorders.MaxX = mainCamera.ViewportToWorldPoint(max).x - movementBorders.maxXOffset;
        movementBorders.MinY = mainCamera.ViewportToWorldPoint(min).y + movementBorders.minYOffset;
        movementBorders.MaxY = mainCamera.ViewportToWorldPoint(max).y - movementBorders.maxYOffset;
    }
}