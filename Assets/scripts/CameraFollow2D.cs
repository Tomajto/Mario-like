using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 3f;
    public Vector3 offset;
    public Transform background;
    public float lookAheadDistance = 3.5f; // Vzdálenost, o kterou se kamera posune dopředu

    private float currentLookAhead;

    void LateUpdate()
    {
        if (target != null)
        {
            // Detekce směru pohybu hráče
            float targetVelocityX = target.GetComponent<Rigidbody2D>().linearVelocity.x;
            if (Mathf.Abs(targetVelocityX) > 0.1f)
            {
                currentLookAhead = Mathf.Lerp(currentLookAhead, lookAheadDistance * Mathf.Sign(targetVelocityX), Time.deltaTime * smoothSpeed);
            }
            else
            {
                currentLookAhead = Mathf.Lerp(currentLookAhead, 0, Time.deltaTime * smoothSpeed);
            }

            // Pozice kamery
            Vector3 targetPosition = target.position + offset + new Vector3(currentLookAhead, 0, 0);
            targetPosition.z = transform.position.z;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // Pozice pozadí
            if (background != null)
            {
                background.position = new Vector3(transform.position.x, transform.position.y, background.position.z);
            }
        }
    }
}

