using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public int msHealth;
    public int msGuard;
    public int attackDamage;
    public Sprite sprite;

    public void Setup(MonsterInfo info)
    {
        monsterName = info.name;
        msHealth = info.msHealth;
        msGuard = info.msGuard;
        attackDamage = info.msDamage;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = info.sprite;
    }

    public void TakeDamage(int damage)
    {
        if (msGuard > 0)
        {
            int remainingDamage = damage - msGuard;
            msGuard -= damage;

            if (msGuard < 0)
            {
                msGuard = 0;
            }

            if (remainingDamage > 0)
            {
                msHealth -= remainingDamage;
            }
        }
        else
        {
            msHealth -= damage;
        }

        if (msHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ���� ��� ó�� ����
        MonsterManager.Inst.RemoveMonster(this);
    }
}
