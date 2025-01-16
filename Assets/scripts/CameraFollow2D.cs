using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.05f;
    public Vector3 offset; 
    public Transform background;

    void LateUpdate()
    {
        if (target != null)
        {
            //target
            Vector3 targetPosition = target.position + offset;
            targetPosition.z = transform.position.z; 
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            //background
            if (background != null)
            {
                background.position = new Vector3(transform.position.x, transform.position.y, background.position.z);
            }
        }
    }
}
