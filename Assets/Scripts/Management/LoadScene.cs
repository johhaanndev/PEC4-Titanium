using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void PlayLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayBoss()
    {
        SceneManager.LoadScene("LevelBoss");
    }

    public void PlayScapeLevel()
    {
        SceneManager.LoadScene("LevelScape");
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
