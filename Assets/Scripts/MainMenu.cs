using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Scores()
    {
        SceneManager.LoadScene("Scores");
    }

    public void Play()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
