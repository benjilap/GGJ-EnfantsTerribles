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
        if (Input.GetKey(KeyCode.Q))
        {
        myBody.velocity = new Vector2(-1*floater,myBody.velocity.y);

        }
    
        if (Input.GetKey(KeyCode.D))
        {
            myBody.velocity = new Vector2(1*floater, myBody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            //Output the message
            Debug.Log("Ground is here!");
            isGrounded = true;
            jump = false;
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            myBody.AddForce(Vector2.up*thrust);
            jump = true;
            isGrounded = false;
        }
    }
}
