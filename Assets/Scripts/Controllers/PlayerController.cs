using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;

    [Header("Shooting")]
    public Transform firePoint;
    public GameObject bolt;                    // Your light bullet prefab
    public float fireRate = 5f;                // Shots per second
    public float bulletSpeed = 20f;

    [Header("Light Energy")]
    // We use PlayerStats for energy management
    private PlayerStats player;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerStats>();

        // Top-down physics setup
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // === MOVEMENT INPUT ===
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        // === AIMING (Mouse) ===
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        Vector2 aimDirection = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // === SHOOTING ===
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && player.energy > 3f)
        {
            Shoot();
        }

        // Optional: Hold E to heal (from your old code)
        if (Input.GetKey("e") && player.energy > 0 && player.canHeal())
        {
            float amount = Time.deltaTime * 10f;
            player.heal(amount);
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.velocity = moveDirection * moveSpeed;
    }

    void Shoot()
    {
        nextFireTime = Time.time + 1f / fireRate;
        player.spendEnergy(3f);

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position;

        Debug.Log($"[Shoot] Attempting to spawn bolt at {spawnPos}");

        GameObject newBolt = Instantiate(bolt, spawnPos, transform.rotation);

        if (newBolt == null)
        {
            Debug.LogError("[Shoot] Instantiate failed - bolt prefab is null!");
            return;
        }

        Debug.Log($"[Shoot] Bolt instantiated successfully! Name: {newBolt.name}");

        BoltScript boltScript = newBolt.GetComponent<BoltScript>();
        if (boltScript == null)
        {
            Debug.LogError("[Shoot] Bolt is missing BoltScript component!");
        }
    }
}