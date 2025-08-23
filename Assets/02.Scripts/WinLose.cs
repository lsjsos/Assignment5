using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnReplayPressed()
    {
        SceneManager.LoadScene("Assignment5");
    }

    public void Quit()
    {
        print("quitting");
        Application.Quit();
    }
}
