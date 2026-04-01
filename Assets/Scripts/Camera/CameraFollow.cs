using UnityEngine;

public class CameraFollowSimplified : MonoBehaviour
{
    [SerializeField] private Transform target;  
    [SerializeField] private float followSpeed = 5f; 

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 newPosition = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }
}