
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    private GameObject[] eny;
    private GameObject g;
    private AudioSource finishSound;
    public UnityEngine.SceneManagement.Scene scene;
    public double total = 0;
    public double died = 0;
    string curName;

    [Header("Kill")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 100;

    void Start()
    {
        finishSound = GetComponent<AudioSource>();
        g = GameObject.FindGameObjectWithTag("Eny");
        eny = GameObject.FindGameObjectsWithTag("Eny");
        curName = SceneManager.GetActiveScene().name;
        total = totalEnemies(eny);
    }

    private double totalEnemies(GameObject[] eny)
    {
        foreach (GameObject enemy in eny)
        {
            total++;
            //currentSouls.text = total.ToString();
        }
        return total;
    }
    private bool isAll(GameObject[] eny)
    {
        //double died = 0;
        foreach (GameObject enemy in eny)
        {
            if (enemy == null)
            {
                died++;
            }
        }
        if (died >= total && curName.Equals("Tutorial"))
        {
            return true;
        } else if (died >= total && curName.Equals("Level1"))
        {
            return true;
        } else if (died >= total && curName.Equals("Level2"))
        {
            return true;
        } else if (died >= total && curName.Equals("Boss"))
        {
            return true;
        }
        else if (died >= total && curName.Equals("Level3"))
        {
            return true;
        }
        else if (died >= total && curName.Equals("Level4"))
        {
            return true;
        }
        else if (died >= total && curName.Equals("Level5"))
        {
            return true;
        }
        else if (died >= total && curName.Equals("Extra"))
        {
            return true;
        }
        return false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (curName.Equals("Extra"))
            {
                GameData gameData = SaveManager.instance.gameData;
                gameData.enemiesStatus.Clear();
                finishSound.Play();
                Invoke("CompleteLevel", 1f);
            }
            else
            {
                if (isAll(eny))
                {
                    GameData gameData = SaveManager.instance.gameData;
                    gameData.enemiesStatus.Clear();
                    finishSound.Play();
                    Invoke("CompleteLevel", 1f);
                }
            }
        }
    }
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
