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

    private CameraShake shake;
    private CameraRecoil recoil;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        shake = GetComponent<CameraShake>();  
        recoil = GetComponent<CameraRecoil>();
    }

    private void LateUpdate()
    {
        if (PauseManager.Instance.isPaused) return;
        if (target == null) return;

        Vector3 desiredPosition = Vector3.Lerp(transform.position + recoil.CurrentRecoilOffset, target.position, followSpeed * Time.deltaTime);
        desiredPosition.z = transform.position.z;

        Vector2 viewSize = new Vector2(
            mainCamera.orthographicSize * mainCamera.aspect,
            mainCamera.orthographicSize
        );
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(desiredPosition.x, leftLimit + viewSize.x, rightLimit - viewSize.x),
            Mathf.Clamp(desiredPosition.y, bottomLimit + viewSize.y, topLimit - viewSize.y),
            desiredPosition.z
        );

        Vector3 finalPosition = clampedPosition + (shake != null ? shake.CurrentShakeOffset : Vector3.zero);
        transform.position = finalPosition;
    }
}