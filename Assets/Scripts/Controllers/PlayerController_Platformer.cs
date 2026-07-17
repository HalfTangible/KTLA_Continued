using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController_Platformer : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
    private float hMove;

    private bool isGrounded;
    public Transform feetPos;
    public Transform leftHandPos;
    public Transform rightHandPos;
    public float checkRadius;

    public LayerMask whatIsGround;

    private bool isJumping;
    private bool canWallJumpLeft;
    private bool canWallJumpRight;
    private float jumpTimeCounter;
    public float jumpTime;
    public float wallJumpTimeMax;
    private float wallJumpTime;
    private bool isWallJumping;
    public float acceleration;

    public Transform firePoint;
    public Transform indicator;

    public GameObject bolt;

    private PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        acceleration = 1f;
        speed = 7f;
        jumpTime = 1f;
        jumpForce = 7f;
        checkRadius = 0.25f;
        wallJumpTimeMax = .1f;
        wallJumpTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (rb.rotation.z != 0)
        //    StandUp();

        //This works for horizontal movement but need to also jump to have a platformer.
        hMove = Input.GetAxisRaw("Horizontal");

        //float h = hMove * speed * Time.deltaTime;

        //Vector3 move = new Vector3(hMove, 0f, 0f);
        //UnityEngine.Debug.Log("Current rb.velocity.y = " + rb.velocity.y);

        if(rb.velocity.x > speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }



        if (wallJumpTime > 0 && isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            wallJumpTime -= Time.deltaTime;
            //UnityEngine.Debug.Log("wallJumpTime: " + wallJumpTime);
        } else if (!isGrounded && hMove == 0) {
            //if not on the ground and not inputting, then the player will just move in whatever direction they were going
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        else {
            //If you are grounded or entering movement you'll head in a particular direction.
            rb.velocity = new Vector2(hMove * speed, rb.velocity.y);
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        canWallJumpLeft = Physics2D.OverlapCircle(rightHandPos.position, checkRadius, whatIsGround);
        canWallJumpRight = Physics2D.OverlapCircle(leftHandPos.position, checkRadius, whatIsGround);

        
        if (Input.GetButtonDown("Fire2"))
        {
            Shoot2();
            //UnityEngine.Debug.Log("isJumping: " + isJumping + "," + "wallJumpLeft: " + canWallJumpLeft + "," + "wallJumpRight: " + canWallJumpRight + "," + "isGrounded: " + isGrounded + ",");
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //rb.velocity = Vector2.up * jumpForce;
            //UnityEngine.Debug.Log("Jump check 1");
        }

        
        if (!isGrounded && canWallJumpLeft && Input.GetKeyDown(KeyCode.Space))
        {
            WallJumpLeft(true);

        }

        if (!isGrounded && canWallJumpRight && Input.GetKeyDown(KeyCode.Space))
        {
            WallJumpLeft(false);
        }

        //Allows a higher jump when holding down space.
        /*if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            higherJump();
        }*/


        if (Input.GetKeyUp(KeyCode.Space))
        {
            
            //UnityEngine.Debug.Log("KeyUp");
            isJumping = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot1();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Shoot2();
        }

        //Light absorbtion (q) is handled in LightStats since the distance to the light source matters.

        if (Input.GetKey("e") && player.energy > 0 && player.canHeal())
        {
            float amount = Time.deltaTime * 10;
            player.heal(amount);

        }
        
    }

    void WallJumpLeft(bool left) {
        //isJumping = true;
        jumpTimeCounter = jumpTime;
        Vector2 direction = (Vector2.up + Vector2.right);

        if (left)
            direction = (Vector2.up + Vector2.left);


        rb.velocity = direction * jumpForce;

        Vector2 temp = direction * jumpForce;
        wallJumpTime = wallJumpTimeMax;
        //UnityEngine.Debug.Log("Wall jump method:" + temp);
        isWallJumping = true;
    }



    void Shoot1()
    {
        //Fire blast that lights up an area
        if(player.energy > 3) {

            player.spendEnergy(3);
            Instantiate(bolt, firePoint.position, indicator.rotation); //shooting logic
        
        }
    }

    void Shoot2()
    {
        
    }

    //Allows higher jump when holding down space.
    /*  void higherJump()
        {
            /*
            
                if (jumpTimeCounter > 0)
                {
                    //rb.velocity = Vector2.up * jumpForce;
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;

                    UnityEngine.Debug.Log("If 2");
                }
                else
                {
                    isJumping = false;

                    UnityEngine.Debug.Log("If, else 2");
                }
            
        }
        */
}
