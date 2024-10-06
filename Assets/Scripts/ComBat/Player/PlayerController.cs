using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int pHealth = 20;
    [SerializeField] public int pGuard = 2;

    public void TakeDamage(int damage)
    {
        if (pGuard > 0)
        {
            int remainingDamage = damage - pGuard;
            pGuard -= damage;

            if (pGuard < 0)
            {
                pGuard = 0;
            }

            if (remainingDamage > 0)
            {
                pHealth -= remainingDamage;
            }
        }
        else
        {
            pHealth -= damage;
        }

        if (pHealth <= 0)
        {
            Die();
        }
    }

    public void IncreaseGuard(int guardValue)
    {
        pGuard += guardValue;
        //Debug.Log("�������� " + guardValue + ". �� ����: " + pGuard);
    }

    void Die()
    {
        Debug.Log("Player is dead!");
        SceneManager.LoadScene("DeadScene");
        // ���� ���� ó�� ���� ���⿡ �߰�
    }
}
