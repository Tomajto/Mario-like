using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 40f; // Degrees to rotate per second

    private float timeSinceLastRotation = 0f;

    void Update()
    {
        // Increment the timer by the time elapsed since the last frame
        timeSinceLastRotation += Time.deltaTime;

        // Rotate the coin once every second
        if (timeSinceLastRotation >= 0.05f)
        {
            transform.Rotate(0, rotationSpeed, 0); 
            timeSinceLastRotation = 0f; 
        }
    }
}
