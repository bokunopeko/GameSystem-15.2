using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Play,
    Pause
}

public class GameManager : MonoBehaviour
{
    //this variable will track the current state of the game
    public static GameState currentState = GameState.Play;

    private PauseUI pauseUI;
    void Start()
    {
        pauseUI = FindObjectOfType<PauseUI>();
        ApplyGameState();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        //do different behavior depending on the value of currentState
        switch (currentState)
        {
            //if (currentState == GameSate.Mainmenu)
            case GameState.MainMenu:
                break; //do nothing
            //if (currentState = Gamestate.Play)
            case GameState.Play:
                currentState = GameState.Pause; // go to pause
                break;
            case GameState.Pause:
                currentState = GameState.Play; // go back to play
                break;
            default:
                break;
        }
        ApplyGameState();
    }

    private void ApplyGameState()
    {
        switch (currentState)
        {
            case GameState.MainMenu:
                break;
            case GameState.Play:
                pauseUI.SetPauseScreen(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1; //because we used rigidbody to move, deltaTime = 1 is normal speed, and 0 = time stop
                break;
            case GameState.Pause:
                pauseUI.SetPauseScreen(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0; // 0 means time stopped 
                break;
            default:
                break;
        }
    }

}
