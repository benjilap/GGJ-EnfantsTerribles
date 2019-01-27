﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    public void LoadScene(string LevelName) 
    {
        SceneManager.LoadScene(LevelName);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

 
}
