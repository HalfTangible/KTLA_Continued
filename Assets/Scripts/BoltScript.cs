using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltScript : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    private float timeLeft = 8f;
    public float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        timeLeft = 5;
        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
            ThisDies();

    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyStats enemy = hitInfo.GetComponent<EnemyStats>();
        
         if(enemy != null){
            enemy.takeDamage(damage);
         }

        ThisDies();

    }

    void ThisDies()
    {
        Destroy(gameObject);
    }
}
