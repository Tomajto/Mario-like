using UnityEngine;

public class Goblin : MonoBehaviour
{
    public float speed = 2f;
    public Transform pointA, pointB;
    private Vector3 target;
    private Rigidbody2D rb;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("PointA or PointB is not assigned!", this);
            return;
        }

        target = pointA.position;

        // Cache Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
            rb.gravityScale = 0; // Ensures no unintended gravity effects
        }
    }

    void Update()
    {
        if (pointA == null || pointB == null) return; // Prevents errors if points are missing

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f) // More precise stopping condition
        {
            if (target == pointA.position)
            {
                target = pointB.position;
            }
            else
            {
                target = pointA.position;
            }
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
