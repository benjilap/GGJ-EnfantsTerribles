using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player") {


            if (this.name == "BeginLevel")
            {
                Debug.Log("3");

                if (this.transform.parent.GetComponent<LevelNum>().levelNum == GameObject.FindObjectOfType<CameraMove>().gameLevel + 1)
                {
                    GameObject.FindObjectOfType<CameraMove>().previousLevel = GameObject.FindObjectOfType<CameraMove>().gameLevel;
                }
            }
        }
    }
}
