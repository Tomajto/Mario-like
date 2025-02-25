using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float chaseSpeed = 3f;
    public Transform pointA, pointB;
    private Vector3 target;
    private Rigidbody2D rb;
    private Transform player;

    public float detectionRadius = 5f;
    public float attackRange = 1f;
    public int damage = 1;
    public float knockbackForce = 5f;

    private bool isChasing = false;
    private bool facingRight = true; // Track direction

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA or PointB is not assigned!", this);
            return;
        }

        target = pointA.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (rb != null)
        {
            rb.freezeRotation = true; // Prevents any rotation
            rb.gravityScale = 1;
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance <= attackRange)
        {
            Attack();
        }
        else if (playerDistance <= detectionRadius)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            Patrol();
        }
    }

    void Patrol()
    {
        float direction = target.x - transform.position.x;
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * speed, rb.linearVelocity.y);

        if (Mathf.Abs(direction) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
            FlipTowards(target.x);
        }
    }

    void ChasePlayer()
    {
        float direction = player.position.x - transform.position.x;
        rb.linearVelocity = new Vector2(Mathf.Sign(direction) * chaseSpeed, rb.linearVelocity.y);
        FlipTowards(player.position.x);
    }

    void Attack()
    {
        CharacterHealth playerHealth = player.GetComponent<CharacterHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            KnockbackPlayer();
        }
    }

    void KnockbackPlayer()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDir = (player.position - transform.position).normalized;
            playerRb.linearVelocity = new Vector2(knockbackDir.x * knockbackForce, playerRb.linearVelocity.y + 2);
        }
    }

    void FlipTowards(float targetX)
    {
        bool shouldFaceRight = targetX > transform.position.x;

        if (shouldFaceRight != facingRight) // Flip only if needed
        {
            facingRight = shouldFaceRight;
            transform.localScale = new Vector3(facingRight ? 1 : -1, 1, 1); // Flips only on X, prevents Z rotation
        }
    }
}
