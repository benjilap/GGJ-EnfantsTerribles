using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{

    [SerializeField]
    private float speed;
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
   public GameObject myActiveWall;
    public float TimeWall = 1;

    //AFK

    bool startTimerAfk;

    float setStartTimerAfk;

    float TimerAfk;
    public float TimerLimitAfk;

    public float scaleModifier = 0.01f;
    public float wallJumpPower;

        public    Vector2 newDirJump ;


    Animator Movement;

    GameObject myObject;

    Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
        myObject = this.gameObject;
        myBody = this.GetComponent<Rigidbody2D>();
        DirJump = (Vector2.up * thrust);
        DirRight = new Vector2(1 * speed, myBody.velocity.y);
        DirLeft = new Vector2(-1 * speed, myBody.velocity.y);
        Movement = this.transform.GetChild(0).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (activeL == false)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                myBody.velocity = new Vector2(-1 * speed, myBody.velocity.y);
                Movement.SetInteger("Movement", 2);
            }
        }


        if (activeR == false)
        {

            if (Input.GetKey(KeyCode.D))
            {
                myBody.velocity = new Vector2(1 * speed, myBody.velocity.y);
                Movement.SetInteger("Movement", 1);
            }
        }

        if(myBody.velocity.magnitude < 1)
        {
            Movement.SetInteger("Movement", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        Debug.Log(myActiveWall);

        if (wallJump == true)
        {

            if (startTimer == false)
            {
                startTimer = true;
                setStartTimer = Time.time;
            }
            Timer = Time.time - setStartTimer;

            if (Timer >= TimeWall)
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

                    newDirJump = new Vector2(DirLeft.x * wallJumpPower, DirJump.y);
                }

                if (myActiveWall.transform.position.x < transform.position.x)
                {

                    newDirJump = new Vector2(DirRight.x * wallJumpPower, DirJump.y);
                }

                myBody.AddForce(newDirJump);
                //

            }
        }

        if (transform.localScale.x <= 1 && transform.localScale.x > 0.2f)
        {

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

        if (transform.localScale.x <= 0.2f)
        {
            Death();
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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Water")
        {
            Death();

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

    void Death()
    {
        this.transform.Find("PlayerDeath").GetComponent<ParticleSystem>().Play();

        if (GameObject.FindObjectOfType<Transition>().GetComponent<Transition>().setTransition == false)
        {
            GameObject.FindObjectOfType<Transition>().GetComponent<Transition>().setTransition = true;
        }
        Destroy(this.gameObject, 0.3f);
    }

    
}
