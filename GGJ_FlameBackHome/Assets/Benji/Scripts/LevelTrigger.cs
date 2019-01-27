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


                if (this.transform.parent.GetComponent<LevelNum>().levelNum == GameObject.FindObjectOfType<CameraMove>().gameLevel + 1)
                {
                    other.transform.parent.localScale = new Vector3(1, 1, 1);
                    this.transform.GetChild(0).GetComponent<Animator>().SetBool("Lighted", true);
                    GameObject.FindObjectOfType<CameraMove>().previousLevel = GameObject.FindObjectOfType<CameraMove>().gameLevel;
                    GameObject.FindObjectOfType<CameraMove>().playerSpawn = this.transform;
                }
            }
        }
    }
}
