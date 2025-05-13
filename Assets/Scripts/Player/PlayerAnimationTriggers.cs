using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(2, null);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                CharacterStats targetStats = enemy.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    player.stats.DoDamage(targetStats);
                    ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);
                    if (weaponData != null)
                        weaponData.Effect(targetStats.transform);
                }
                else
                {
                    Debug.LogWarning("Target does not have CharacterStats!");
                }
            }
        }
    }
    private void WeaponEffect()
    {
        
    }
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
