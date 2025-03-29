using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Sprite sprite1; // First frame of the animation
    public Sprite sprite2; // Second frame of the animation
    public float switchTime = 0.2f; // Time interval between frames

    private SpriteRenderer spriteRenderer;
    private float timer;
    private bool isUsingSprite1 = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }

        // Set the initial sprite
        spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
        // Check if the player is moving
        if (IsPlayerMoving())
        {
            // Handle animation timing
            timer += Time.deltaTime;

            if (timer >= switchTime)
            {
                // Toggle between the two sprites
                isUsingSprite1 = !isUsingSprite1;
                spriteRenderer.sprite = isUsingSprite1 ? sprite1 : sprite2;

                // Reset the timer
                timer = 0f;
            }
        }
        else
        {
            // Reset to the first sprite when idle
            spriteRenderer.sprite = sprite1;
        }
    }

    // Method to determine if the player is moving
    private bool IsPlayerMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }
}
