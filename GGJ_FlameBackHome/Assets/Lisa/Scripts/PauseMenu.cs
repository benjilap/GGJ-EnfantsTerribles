using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUi;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        PauseUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            PauseUi.SetActive(true);
            Time.timeScale = 0;

        }

        if (!paused)
        {
            PauseUi.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
