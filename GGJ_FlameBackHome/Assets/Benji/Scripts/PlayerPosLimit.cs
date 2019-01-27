using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosLimit : MonoBehaviour
{
    public Vector3 myViewportPos; 

    private void Update()
    {
        myViewportPos = Camera.main.WorldToViewportPoint(transform.position);

        if (myViewportPos.x < 0)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.01f, myViewportPos.y, myViewportPos.z));
        }
        if (myViewportPos.x > 1)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.99f, myViewportPos.y, myViewportPos.z));
        }
        if (myViewportPos.y < 0)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(myViewportPos.x, 0.01f, myViewportPos.z));
        }
        if (myViewportPos.y > 1)
        {
            transform.position = Camera.main.ViewportToWorldPoint(new Vector3(myViewportPos.x, 0.99f, myViewportPos.z));
        }
    }
}
