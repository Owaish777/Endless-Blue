using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -6);
    public float smoothTime = 0.001f;


    Vector3 velocity = Vector3.zero;
    float rotationVelocity;

    void LateUpdate()
    {
        // Desired position
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        desiredPosition.y = offset.y; // Keep camera at a fixed height above the boat

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime 
        );

        // Desired rotation
        float targetAngle = target.eulerAngles.y;
        float currentAngle = transform.eulerAngles.y;

        float smoothedAngle = Mathf.SmoothDampAngle(
            currentAngle,
            targetAngle,
            ref rotationVelocity,
            smoothTime
        );

        transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
    }
}