using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private UI_HealthBar healthBar;
    private ItemDrop myDropSystem;
    public Stat soulsDropAmount;
    public string sceName;

    [Header("Level details")]
    [SerializeField] private int level=1;

    [Range(0f, 1f)]
    [SerializeField] private float percantageModifier=.4f;
    protected override void Start()
    {
        soulsDropAmount.SetDefaultValue(100);
        ApplyLevelModifiers();
        base.Start();
        enemy = GetComponent<Enemy>();
        healthBar = GetComponentInChildren<UI_HealthBar>();
        myDropSystem = GetComponent<ItemDrop>();
        sceName = SceneManager.GetActiveScene().name;
    }

    private void ApplyLevelModifiers()
    {
        Modify(strength);
        Modify(agility);
        Modify(intelligence);
        Modify(vitality);

        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicResistance);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightingDamage);

        Modify(soulsDropAmount);
    }

    private void Modify(Stat _stat)
    {
        for(int i=1; i<level; i++)
        {
            float modifier = _stat.GetValue() * percantageModifier;
            _stat.AddModifier(Mathf.RoundToInt(modifier));  
        }
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        if (currentHealth <= 0)
        {
            Die();
        }
        //enemy.DamageEffect();
    }

    public void ResetStats()
    {
        enemy.cd.enabled = true;
        currentHealth = maxHealth.GetValue() + vitality.GetValue() * 5;
        healthBar.SetHealthBar();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();

        PlayerManager.instance.currency += soulsDropAmount.GetValue();
        myDropSystem.GenerateDrop();
        
        if (!(sceName.Equals("Extra")))
        {
            Destroy(gameObject, 5f);
        }
        
    }
}
