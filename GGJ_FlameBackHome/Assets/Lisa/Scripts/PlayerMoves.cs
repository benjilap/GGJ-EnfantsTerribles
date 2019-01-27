using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{

    [SerializeField]
    private float floater;
    [SerializeField]
    private float thrust;
   
    private Vector2 DirJump, DirRight, DirLeft;


    [HideInInspector]
    bool jump, isGrounded;

    //WallJump
    bool startTimer;
    float setStartTimer;
    float Timer;
    bool wallJump;
    bool activeR, activeL;
    GameObject myActiveWall;

    //AFK
    [SerializeField]
    bool startTimerAfk;
    [SerializeField]
    float setStartTimerAfk;
    [SerializeField]
    float TimerAfk;
    public float TimerLimitAfk;

    public float scaleModifier = 0.01f;




    GameObject myObject;

    Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
        myObject = this.gameObject;
        myBody = this.GetComponent<Rigidbody2D>();
        DirJump = (Vector2.up * thrust);
        DirRight = new Vector2(1 * floater, myBody.velocity.y);
        DirLeft = new Vector2(-1 * floater, myBody.velocity.y);
         }

    // Update is called once per frame
    void Update()
    {
        if (activeL == false)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                myBody.velocity = new Vector2(-1 * floater, myBody.velocity.y);

            }
        }

        if (activeR == false)
        {

            if (Input.GetKey(KeyCode.D))
            {
                myBody.velocity = new Vector2(1 * floater, myBody.velocity.y);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


        if (wallJump == true)
        {
            Vector2 newDirJump = Vector2.zero;

            if (startTimer == false)
            {
                startTimer = true;
                setStartTimer = Time.time;
            }
            Timer = Time.time - setStartTimer;

            if (Timer >= 5)
            {
                if (myActiveWall.transform.position.x > transform.position.x)
                {
                    activeR = true;
                }

                else if (myActiveWall.transform.position.x < transform.position.x)
                {
                    activeL = true;
                }

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (myActiveWall.transform.position.x > transform.position.x)
                {

                    newDirJump = new Vector2(DirJump.x + DirLeft.x * 150, DirJump.y + DirLeft.y);
                }

                if (myActiveWall.transform.position.x < transform.position.x)
                {

                    newDirJump = new Vector2(DirJump.x + DirRight.x * 150, DirJump.y + DirRight.y);
                }

                myBody.AddForce(newDirJump);

            }
        }


        

     

        if (transform.localScale.x <= 1 && transform.localScale.x > 0)
        {
            Debug.Log(myBody.velocity);

            if (myBody.velocity.magnitude <1)

            {

                if (startTimerAfk == false)
                {
                    startTimerAfk = true;
                    setStartTimerAfk = Time.time;
                }
                TimerAfk = Time.time - setStartTimerAfk;

                if (TimerAfk > TimerLimitAfk)

                {
                    myObject.transform.localScale -= new Vector3(scaleModifier, scaleModifier, scaleModifier);
                    startTimerAfk = false;
                }


            }

            else if (myBody.velocity.magnitude >1)


            {
                startTimerAfk = false;
            }


        }

        if (transform.localScale.x <= 0)
        {
            Debug.Log("Perdu");
        }


            

        
    }

    //CollisionGround
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            
            isGrounded = true;
            jump = false;
        }

        if (collision.gameObject.tag == "Mur")
        {
            wallJump = true;
            myActiveWall = collision.gameObject;

        }
        if (collision.gameObject.tag == "Checkpoint")
        {
            myObject.transform.localScale = new Vector3(1, 1, 1);

        }

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mur")
        {

            wallJump = false;
            myActiveWall = null;
            activeL = false;
            activeR = false;
            startTimer = false;

        }

    }






    public void Jump()
    {
        if (isGrounded)
        {
            myBody.AddForce(Vector2.up * thrust);
            jump = true;
            isGrounded = false;
        }

    }


}
