using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //[HideInInspector]
    public int gameLevel;
    public int previousLevel;

    [HideInInspector]
    public Transform playerSpawn;
    Transform currentLevel;
    Transform nextLevel;
    
    LevelNum[] myLevels;

    public GameObject playerPrefab;

    //LerpTimer
    bool startLerp;
    float startTimer;
    float lerpTimer;
    [SerializeField]
    float modifTimer;

    void Start()
    {
        myLevels = GameObject.FindObjectsOfType<LevelNum>();
        foreach(LevelNum myLevel in myLevels)
        {

            if (myLevel.levelNum == 1)
            {
                currentLevel = myLevel.transform;
                playerSpawn = currentLevel.Find("BeginLevel").transform;
                gameLevel = myLevel.GetComponent<LevelNum>().levelNum;


            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevel == null)
        {
            foreach (LevelNum myLevel in myLevels)
            {
                if (myLevel.levelNum == gameLevel + 1)
                {

                    nextLevel = myLevel.transform;
                }
            }
        }

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (GameObject.FindObjectOfType<Transition>().GetComponent<Transition>().setTransition == true)
            {
                GameObject.FindObjectOfType<Transition>().GetComponent<Transition>().setTransition = false;

            }
            if (playerSpawn.transform.GetChild(0).GetComponent<Animator>().GetBool("Lighted") == false)
            {
                playerSpawn.transform.GetChild(0).GetComponent<Animator>().SetBool("Lighted", true);
            }
            Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        }

        if (previousLevel == gameLevel)
        {

            LerpToNewLevel();
        }
    }

    void LerpToNewLevel()
    {
        Vector3 lastCamPos = transform.position;
        if (!startLerp)
        {
            startLerp = true;
            startTimer = Time.time;
        }
        lerpTimer = startTimer - lerpTimer;
        transform.position = Vector3.Lerp(transform.position, new Vector3(nextLevel.position.x, nextLevel.position.y, transform.position.z), lerpTimer*(1/modifTimer));
        if (Vector3.Distance(transform.position, new Vector3(nextLevel.position.x, nextLevel.position.y, transform.position.z)) <= 0.1f)
        {
            startLerp = false;
            transform.position = new Vector3(nextLevel.position.x, nextLevel.position.y, transform.position.z);
            gameLevel += 1;

            foreach (LevelNum myLevel in myLevels)
            {
                if (myLevel.levelNum == gameLevel + 1)
                {
                    Debug.Log("1");
                    nextLevel = myLevel.transform;
                }
            }
        }

    }
}
