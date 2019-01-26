using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenjiMoves : MonoBehaviour
{
    [SerializeField]
    float speedMove=1;
    float velocityModif;

    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb2D.velocity = new Vector2(Input.GetAxis("Horizontal")*speedMove,rb2D.velocity.y);
        }
    }
}
