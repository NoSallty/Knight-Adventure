using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Entity entity=>GetComponentInParent<Entity>();
    private CharacterStats myStats=>GetComponentInParent<CharacterStats>();
    private RectTransform myTransform;
    private Slider slider;
    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();        
        UpdateHealthUI();
    }
    
    private void UpdateHealthUI()
    {
        if (myStats != null)
        {
            slider.maxValue = myStats.GetMaxHealthValue();
            slider.value = myStats.currentHealth;
        }
        else
        {
            Debug.LogWarning("myStats is null!");
        }
    }
    private void OnEnable()
    {
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
    }
    private void OnDisable()
    {
        if(entity!=null)
            entity.onFlipped -= FlipUI;
        if(myStats!=null)
            myStats.onHealthChanged -= UpdateHealthUI;
    }
    private void FlipUI()=>myTransform.Rotate(0, 180, 0);
}
