using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    PlayerStats playerStats;
    float maxHealth;
    float currentHealth;
    Vector2 startScale;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        
        startScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerStats.currentHealth;
        maxHealth = playerStats.maxHealth;

        //transform.localScale.x = (currentHealth / maxHealth) * startScale.x;
        //transform.localScale.y = (currentHealth / maxHealth) * startScale.y;

        transform.localScale = new Vector3(startScale.x, (currentHealth / maxHealth) * startScale.y, 1);

    }
}
