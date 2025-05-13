using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadExtra()
    {
        SceneManager.LoadScene("Extra");
    }
    public void LoadMenuLV()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
