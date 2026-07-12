using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class MoveIndicator : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 mousePos;
    private Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Follow mouse

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //transform.rotation = Quaternion.Euler(0, 0, rotZ-90);
        transform.rotation = Quaternion.Euler(playerPos.rotation.x, playerPos.rotation.y, rotZ-90);

        //Set the indicator's position to be a unit away from the player.
        //transform.position = Vector();

        //transform.RotateAround(playerPos.position, Vector3.up, 20 * Time.deltaTime);

    }
}