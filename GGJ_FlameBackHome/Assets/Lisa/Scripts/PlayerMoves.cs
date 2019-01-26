using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoves : MonoBehaviour
{

    [SerializeField]
    private float floater;
    [SerializeField]
    private float thrust;

    [HideInInspector]
    public int integ;
    string text;
    bool jump, isGrounded;

    //WallJump
    bool startTimer;
    float setStartTimer;
    float Timer;
    bool wallJump;
    bool activeR, activeL;
    GameObject myActiveWall;


    GameObject myObject;

    Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
        myObject = this.gameObject;
        text = myObject.name;
        myBody = this.GetComponent<Rigidbody2D>();
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

        }
    }

    //CollisionGround
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Output the message
            Debug.Log("Ground is here!");
            isGrounded = true;
            jump = false;
        }

        if (collision.gameObject.tag == "Mur")
        {
            wallJump = true;
            myActiveWall = collision.gameObject;

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
