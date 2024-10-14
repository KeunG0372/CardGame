using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int pHealth = 20;
    [SerializeField] public int pGuard = 2;

    private bool isAttacking = false;
    [SerializeField] private float duration = 0.2f;

    GameObject playerObject;

    private void Start()
    {
        playerObject = this.gameObject;
    }

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

    public void Attack(Vector3 monsterPosition)
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformAttack(monsterPosition));
        }
    }

    private IEnumerator PerformAttack(Vector3 monsterPosition)
    {
        isAttacking = true;

        Vector3 originalPosition = transform.position;

        // �÷��̾ ���� �������� �̵�
        Vector3 targetPosition = Vector3.Lerp(originalPosition, monsterPosition, 0.15f);
        transform.DOMove(targetPosition, duration);
        yield return new WaitForSeconds(duration);

        // ���� ��ġ�� �̵�
        transform.DOMove(originalPosition, duration);
        yield return new WaitForSeconds(duration);

        isAttacking = false;
    }
}
