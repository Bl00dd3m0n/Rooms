﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] Doors;//Keeps a list of the Dorrs(This honestly may not be needed and will probably be removed)
    public Vector2[] Boundaries;//Boundaries(Will be used to make sure everything is properly deleted and things are kept within.
    public GameObject player;//Used to keep track of the player
    public bool GameInProgress;//Used to see if the game is still in progress
    // Start is called before the first frame update
    void Start()
    {
        GameInProgress = true;
    }
    //Gets in the cause of the game being over and prints it to the console.
    public void GameOver(string Text)
    {
        Debug.ClearDeveloperConsole();
        Debug.Log(Text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
