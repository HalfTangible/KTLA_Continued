using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float energy;
    public bool killable;
    public float speed;
    public UnityEngine.Rendering.Universal.Light2D glow;
    public bool penalized;
    public float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15;
        currentHealth = maxHealth;
        energy = 1;
        glow = this.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //UnityEngine.Debug.Log("Stats.Update");

        if (currentHealth == 0 && killable)
            ThisDies();

        CheckEnergy();
    }

    public virtual void ThisDies()
    {
        //Destroy(gameObject);
        //UnityEngine.Debug.Log("Stats.ThisDies triggered.");
        //UnityEngine.Debug.Log("Stats.ThisDies triggered.");
        Destroy(gameObject);
    }

    public virtual void CheckEnergy()
    {
        //Depends.
        //Enemies: Meh, maybe die?
        //Torches: Animate their death?
        //Player: Slows down.

        if (energy <= 30)
        {
            penalized = true;
        }
        else
        {
            penalized = false;
        }
    }

    public virtual bool canHeal()
    {
        return currentHealth < maxHealth;
    }

    public void spendEnergy(float amount)
    {
        energy -= amount;
    }

    public void gainEnergy(float amount)
    {
        energy += amount;
    }

    public void takeDamage(float amount)
    {
        currentHealth -= amount;
    }

    public void gainHealth(float amount)
    {
        currentHealth += amount;
    }

    public void gainMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth += amount;
    }

    public void gainSpeed(float amount)
    {
        speed += amount;
    }

    public void loseSpeed(float amount)
    {
        speed -= amount;
    }


}
