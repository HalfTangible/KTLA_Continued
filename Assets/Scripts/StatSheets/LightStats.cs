using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightStats : Stats
{
    PlayerStats playerStats;
    private Vector3 playerPos;
    private GameObject player;
    // Start is called before the first frame update
    public float maxRange;
    void Start()
    {
        energy = 300;
        glow = this.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        maxRange = 5f;
    }

    // Update is called once per frame
    public override void Update()
    {
        glow.intensity = energy * .01f; //As you absorb light from light sources, they fade away.
        playerPos = player.transform.position;

        //If this light has energy and the player is trying to absorb it, check range.
        if (energy > 0 && Input.GetKey("q"))
        {
            if(playerIsInRange()){
                //the player gains a third of the energy drained from this target over time.
                float amount = Time.deltaTime * 15;
                spendEnergy(amount * 3);
                playerStats.gainEnergy(amount);
            }

        }
    }

    bool playerIsInRange()
    {
        //If the player is within 5 units(?) return true.
        
        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance <= maxRange)
            return true;
        
        return false;
    }


}
