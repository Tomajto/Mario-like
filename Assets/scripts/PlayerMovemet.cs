using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 32f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Sprite sprite1; // First frame of the animation
    [SerializeField] private Sprite sprite2; // Second frame of the animation
    [SerializeField] private float switchTime = 0.2f; // Time interval between frames

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
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jumping logic
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
        HandleAnimation(); // Call the animation handler
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 2f, groundLayer);
        return isGrounded;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void HandleAnimation()
    {
        if (Mathf.Abs(horizontal) > 0) // Player is moving
        {
            timer += Time.deltaTime;

            if (timer >= switchTime)
            {
                // Switch between sprites
                isUsingSprite1 = !isUsingSprite1;
                spriteRenderer.sprite = isUsingSprite1 ? sprite1 : sprite2;

                // Reset timer
                timer = 0f;
            }
        }
        else
        {
            // Reset to the first sprite when idle
            spriteRenderer.sprite = sprite1;
        }
    }
}
