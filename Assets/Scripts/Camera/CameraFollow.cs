using UnityEngine;

public class CameraFollowSimplified : MonoBehaviour
{
    [SerializeField] private Transform target;  
    [SerializeField] private float followSpeed = 5f; 
    private Camera mainCamera;
    [Header("Arena Limits")]
    [SerializeField] private float bottomLimit = -5.3f;
    [SerializeField] private float leftLimit = -9.6f;
    [SerializeField] private float topLimit = 5.3f;
    [SerializeField] private float rightLimit = 9.6f;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 newPosition = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        newPosition.z = transform.position.z;

        Vector2 viewSize = new Vector2 (
            mainCamera.orthographicSize * mainCamera.aspect, 
            mainCamera.orthographicSize
        );
        transform.position = new Vector3 (
            Mathf.Clamp(newPosition.x, leftLimit + viewSize.x, rightLimit - viewSize.x),
            Mathf.Clamp(newPosition.y, bottomLimit + viewSize.y, topLimit - viewSize.y),
            newPosition.z
        );
    }
}