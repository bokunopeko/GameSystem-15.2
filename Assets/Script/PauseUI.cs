using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{

    [SerializeField] private GameObject pauseScreen;

    public void SetPauseScreen(bool active)
    {
        pauseScreen.SetActive(active);
    }
}
