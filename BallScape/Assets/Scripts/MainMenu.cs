using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void ItchIO()
    {
        Application.OpenURL("https://victorgg-11.itch.io/ballscape");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
