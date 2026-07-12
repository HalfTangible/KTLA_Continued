using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Goal: make the camera follow the player just a bit behind.
    public GameObject targetToFollow;
    public float maxOffset;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        targetToFollow = GameObject.Find("Player");
        speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        moveTowards(targetToFollow);
    }

    void moveTowards(GameObject target)
    {
        
        Vector3 playerPos = target.transform.position;
        Vector3 newPos = new Vector3(playerPos.x, playerPos.y, -10);
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, speed);
    }

    /*float distanceBetween(GameObject one, GameObject two)
    {
        /*
        //What is the distance formula for cartesian coordinates?
        //The general distance formula in cartesian coordinates is:
        //d = √[(x₂ - x₁)² + (y₂ - y₁)² + (z₂ - z₁)²]

        double a = one.position.x - two.position.x;
        double b = one.position.y - two.position.y;

        double formula = (a * a) + (b * b);


        return (float) Math.Sqrt(formula);

    }*/
}
