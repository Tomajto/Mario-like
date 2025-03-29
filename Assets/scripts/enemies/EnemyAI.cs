using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform pointA, pointB; // Patrol points
    public Transform player; // Player reference
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;
    public float detectionRange = 5f;

    private Transform target;
    private bool isChasing = false;

    private Rigidbody2D rb;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    private bool isGrounded;
    void Start()
    {
        target = pointA; // Start patrolling to Point A
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (!isGrounded) return;

        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance < detectionRange)
        {
            isChasing = true;
        }
        else if (isChasing && playerDistance > detectionRange + 1f)
        {
            isChasing = false;
            target = (Vector2.Distance(transform.position, pointA.position) < Vector2.Distance(transform.position, pointB.position)) ? pointA : pointB;
        }

        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
        MoveTowards(target.position, patrolSpeed);
    }

    void ChasePlayer()
    {
        MoveTowards(player.position, chaseSpeed);
    }

    void MoveTowards(Vector2 destination, float speed)
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
    }
}
