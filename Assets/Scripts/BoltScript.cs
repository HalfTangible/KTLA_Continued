using UnityEngine;

public class BoltScript : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    public float damage = 5f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (rb != null)
        {
            rb.velocity = transform.right * speed;
        }

        // Ignore collision with player for a short time
        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider, true);
        }

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
            return;   // Extra safety

        EnemyStats enemy = hitInfo.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.takeDamage(damage);
        }

        Destroy(gameObject);
    }
}