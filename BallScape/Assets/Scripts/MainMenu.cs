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

    public void LinkedInOriol()
    {
        Application.OpenURL("https://www.linkedin.com/in/oriol-capdevila/");
    }

    public void LinkedInVictor()
    {
        Application.OpenURL("https://www.linkedin.com/in/victorsegurablanco/");
    }

    public void Leaderboard()
    {

    }
}
