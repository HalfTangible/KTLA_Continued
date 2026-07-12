using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject player;
    public Vector3 playerPos;
    public float iframes;
    public float damage;
    public float speed;
    public float step;
    public float aggroDistance;

    // Start is called before the first frame update
    void Start()
    {
        damage = 3;
        iframes = 3; //Let the camera come down
        player = GameObject.Find("Player");
        speed = 4f;
        aggroDistance = 10;
    }

    // Update is called once per frame
    void Update()
    {

        playerPos = player.transform.position;

        if (iframes > 0)
        {
            //Need some indication that it can't hurt you right now
            iframes -= Time.deltaTime;
            //Move away from player
            //Vector3 direction = this.transform.position - playerPos; //Should be away from.
            transform.position = Vector3.MoveTowards(transform.position, playerPos, -step);
            //UnityEngine.Debug.Log("Move away from player");

        }
        else if (Vector3.Distance(playerPos, this.transform.position) < aggroDistance)
        {
            step = speed * Time.deltaTime;
            //Move towards player
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
            //UnityEngine.Debug.Log("Move toward player");
        }
    }
    /*
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        UnityEngine.Debug.Log("OnTriggerEnter2D (EnemyController)");
        PlayerStats player = hitInfo.GetComponent<PlayerStats>();

        if (player != null && iframes <= 0)
        {
            player.takeDamage(damage);
            iframes = 3f; //Hurts when hitting, but can only hit the player every three seconds.
        }


    }*/

    
    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();

        //UnityEngine.Debug.Log("OnCollisionEnter");
        
        if(player && iframes <= 0)
        {
            //UnityEngine.Debug.Log("OnCollisionEnter: do damage");
            player.takeDamage(damage);
            iframes = 3f; //Hurts when hitting, but can only hit the player every three seconds.
        }
    }

    

}
