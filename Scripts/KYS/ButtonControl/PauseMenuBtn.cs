using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuBtn : MonoBehaviour
{
    public GameObject pauseMenu;

    public void TogglePauseMenu()
    {
        if (pauseMenu != null)
        {
            bool isActive = pauseMenu.activeSelf;
            pauseMenu.SetActive(!isActive);
        }
    }
}
