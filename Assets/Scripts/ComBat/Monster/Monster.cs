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

    public void Setup(MonsterInfo info)
    {
        monsterName = info.name;
        msHealth = info.msHealth;
        msGuard = info.msGuard;
        attackDamage = info.msDamage;
        // sprite = info.sprite; // 만약 스프라이트가 추가되면 사용
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
        // 몬스터 사망 처리 로직
        MonsterManager.Inst.RemoveMonster(this);
    }
}
