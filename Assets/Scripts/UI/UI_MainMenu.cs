using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    //[SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start()
    {
        //if (SaveManager.instance.HasSavedData() == false)
        //{
        //    continueButton.SetActive(false);
        //}
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void NewGame()
    {
        SaveManager.instance.DeleteSaveData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void ExitGame()
    {
        fadeScreen.FadeOut();
        Application.Quit();
    }

    public void LoadMenuLV()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("LevelSelect");
    }
    public void LoadMain()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadTuto()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadLevel_1()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel_2()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel_3()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel_4()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel_5()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Level5");
    }
    public void LoadLevelBoss()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Boss");
    }
    public void LoadExtra()
    {
        fadeScreen.FadeOut();
        SceneManager.LoadScene("Extra");
    }
    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
