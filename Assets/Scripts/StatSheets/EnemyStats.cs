using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    
    
    // Start is called before the first frame update
    void Start()
    {
        energy = 100;
        maxHealth = 15;
        currentHealth = 15;
        killable = true;
       
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

}
