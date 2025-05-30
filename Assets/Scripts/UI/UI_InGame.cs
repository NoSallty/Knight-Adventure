﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image dashImage;
    [SerializeField] private Image parryImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image blackholeImage;
    [SerializeField] private Image flaskImage;

    [Header("Souls info")]
    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float soulsAmount;
    [SerializeField] private float increaseRate = 100;

    [Header("Enemy info")]
    //[SerializeField] private TextMeshProUGUI EnemyKilled;
    [SerializeField] private TextMeshProUGUI RemainEnemy;
    [SerializeField] private TextMeshProUGUI Enemys;
    [SerializeField] private double EnemyTotal;
    [SerializeField] private double EnemyKilled;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI? timerText;
    [SerializeField] public float timeRemain;

    private SkillManager skills;
    private GameObject[] eny;
    void Start()
    {
        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthUI;
        eny = GameObject.FindGameObjectsWithTag("Eny");
        EnemyTotal = eny.Length;
        Enemys.text = EnemyTotal.ToString();

        skills = SkillManager.instance;
        timeRemain = 0;
        //UpdateEnysUI();
    }
    void FixedUpdate()
    {
        UpdateEnysUI();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSoulsUI();
        UpdateTimerText();

        if (Input.GetKeyDown(KeyCode.LeftShift) && skills.dash.dashUnlocked)
            SetCooldownOf(dashImage);
        if (Input.GetKeyDown(KeyCode.Q) && skills.parry.parryUnlocked)
            SetCooldownOf(parryImage);
        if (Input.GetKeyDown(KeyCode.F) && skills.crystal.crystalUnlocked)
            SetCooldownOf(crystalImage);
        if (Input.GetKeyDown(KeyCode.Mouse1) && skills.sword.swordUnlocked)
            SetCooldownOf(swordImage);
        if (Input.GetKeyDown(KeyCode.R) && skills.blackhole.blackholeUnlocked)
            SetCooldownOf(blackholeImage);
        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetEquipment(EquipmentType.Flask) != null)
            SetCooldownOf(flaskImage);
        CheckCooldownOf(dashImage, skills.dash.cooldown);
        CheckCooldownOf(parryImage, skills.parry.cooldown);
        CheckCooldownOf(crystalImage, skills.crystal.cooldown);
        CheckCooldownOf(swordImage, skills.sword.cooldown);
        CheckCooldownOf(blackholeImage, skills.blackhole.cooldown);
        CheckCooldownOf(flaskImage, Inventory.instance.flaskCooldown);

        timeRemain += Time.deltaTime;
        //UpdateTimerText();
    }

    void UpdateTimerText()
    {
        if (timerText != null) 
        {
            int minutes = Mathf.FloorToInt(timeRemain / 60);
            int seconds = Mathf.FloorToInt(timeRemain % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void UpdateSoulsUI()
    {
        if (soulsAmount < PlayerManager.instance.GetCurrency())
            soulsAmount += Time.deltaTime * increaseRate;
        else
            soulsAmount = PlayerManager.instance.GetCurrency();

        currentSouls.text = ((int)soulsAmount).ToString();
    }

    //private double totalEnemies(GameObject[] eny)
    //{
    //    foreach (GameObject enemy in eny)
    //    {
    //        if (enemy != null)
    //        {
    //            EnemyTotal++;
    //        }
    //    }
    //    return EnemyTotal;
    //}
    //private double killedEnemies()
    //{
    //    eny = GameObject.FindGameObjectsWithTag("Eny");
    //    EnemyKill = (int)EnemyTotal - eny.Length;
    //    return EnemyKill;
    //}

    private void UpdateEnysUI()
    {
        eny = GameObject.FindGameObjectsWithTag("Eny");
        //EnemyTotal = totalEnemies(eny);
        //Enemys.text = EnemyTotal.ToString();
        //EnemyKill = killedEnemies();
        //EnemyKilled.text = EnemyKill.ToString();
        RemainEnemy.text = eny.Length.ToString();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue=playerStats.GetMaxHealthValue();
        slider.value = playerStats.currentHealth;
    }
    private void SetCooldownOf(Image _image)
    {
        if (_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }
    private void CheckCooldownOf(Image _image,float _cooldown)
    {
        if(_image.fillAmount>0)
            _image.fillAmount-=1/_cooldown*Time.deltaTime;
    }
}
