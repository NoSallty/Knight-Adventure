using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start()
    {
        if (SaveManager.instance.HasSavedData() == false)
        {
            continueButton.SetActive(false);
        }
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
        Application.Quit();
    }

    public void LoadMenuLV()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadLevel_1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel_2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel_3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel_4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel_5()
    {
        SceneManager.LoadScene("Level5");
    }
    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
