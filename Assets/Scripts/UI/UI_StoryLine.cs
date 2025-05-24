using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_StoryLine : MonoBehaviour
{
    [SerializeField] private string sceneName = "Tutorial";
    //[SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start()
    {
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    public void NewGame()
    {
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
    /*public void ExitGame()
    {
        Application.Quit();
    }*/
   IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
