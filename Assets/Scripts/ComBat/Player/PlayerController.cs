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
        //Debug.Log("가드증가 " + guardValue + ". 총 가드: " + pGuard);
    }

    void Die()
    {
        Debug.Log("Player is dead!");
        SceneManager.LoadScene("DeadScene");
        // 게임 오버 처리 등을 여기에 추가
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

        // 플레이어를 몬스터 방향으로 이동
        Vector3 targetPosition = Vector3.Lerp(originalPosition, monsterPosition, 0.15f);
        transform.DOMove(targetPosition, duration);
        yield return new WaitForSeconds(duration);

        // 원래 위치로 이동
        transform.DOMove(originalPosition, duration);
        yield return new WaitForSeconds(duration);

        isAttacking = false;
    }
}
