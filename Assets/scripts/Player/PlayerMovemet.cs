using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 21f;
    private bool isFacingRight = true;
    private Animator anim;
    private bool grounded;
    private float airTime;
    private float maxAirTime = 0.2f; // Maxim�ln� �as ve vzduchu, po kter�m se zak�e pohyb

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Sprite sprite1;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }

        spriteRenderer.sprite = sprite1;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            anim.SetTrigger("jump");
        }

        if (Input.GetButton("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity += new Vector2(0, jumpingPower * Time.deltaTime);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
        anim.SetBool("run", horizontal != 0);
        anim.SetBool("grounded", IsGrounded());
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            airTime = 0f; // Reset air time when grounded
            rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
        }
        else
        {
            airTime += Time.fixedDeltaTime;
            if (airTime < maxAirTime)
            {
                rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
            }
        }
    }

    private bool IsGrounded()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        return grounded;
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

    public bool canAttack()
    {
        return horizontal == 0 && IsGrounded();
    }
}



