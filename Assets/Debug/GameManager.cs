using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DebugTest
{
    public class GameManager : MonoBehaviour
    {
        ///////
        /////// 18 Errors are present from this point down!
        /////// Nothing needs to be relocated, fully removed, or fully rewritten.
        ///////
        public enum GameState
        {
            Menu,
            Play,
            Pause
        }

        [SerializeField] private GameObject pauseScreen;
        public static GameState currentState = GameState.Play;

        private void Start()
        {
            ApplyGameState();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            switch (currentState)
            {
                case GameState.Menu:
                    break;
                case GameState.Play:
                    currentState = GameState.Pause;
                    break;
                case GameState.Pause:
                    currentState = GameState.Play;
                    break;
            }
            ApplyGameState();
        }

        private void ApplyGameState()
        {
            switch (currentState)
            {
                case GameState.Menu:
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                case GameState.Play:
                    pauseScreen.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    break;
                case GameState.Pause:
                    pauseScreen.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    break;
            }
        }
        ///////
        /////// There are no errors beyond this point!
        ///////
    }
}