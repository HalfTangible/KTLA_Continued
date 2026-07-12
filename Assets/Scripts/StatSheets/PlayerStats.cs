using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerStats : Stats
{

    public float speedPenalty;
    public GameObject gameOverScreen;

    void Start()
    {
        maxHealth = 15;
        currentHealth = maxHealth;
        energy = 100;
        glow = this.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        gameOverScreen = GameObject.Find("Game Over Screen");
        gameOverScreen.SetActive(false);
    }

    public override void Update()
    {
        glow.intensity = energy * .01f; //The more energy you spend, the weaker your light becomes, but the more you gain, the brighter you shine.
        glow.falloffIntensity = 1 - (.6f * (currentHealth/maxHealth)); //As you get more injured, the falloff strength of the light increases.

        CheckEnergy();

        if (currentHealth <= 0)
        {
            ThisDies();
        }
    }

    public override void ThisDies()
    {

        //Game over screen.
        //Needs to override to prevent player from being destroyed?
        GameOver();


    }

    public override void CheckEnergy(){

        base.CheckEnergy();
        if (penalized)
        {
            currentSpeed = speed - speedPenalty;
        }
        else
        {
            currentSpeed = speed;
        }

    }

    public override bool canHeal()
    {
        if (base.canHeal() && energy > 0)
        {
            return true;
        }
        else
            return false;

    }

    public void heal(float amount)
    {
        gainHealth(amount);
        spendEnergy(amount);
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

}
