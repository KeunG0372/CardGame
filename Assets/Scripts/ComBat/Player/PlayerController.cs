using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int pMaxHealth = 20;
    [SerializeField] public int pHealth = 20;
    [SerializeField] public int pGuard = 2;

    private bool isAttacking = false;
    [SerializeField] private float duration = 0.2f;

    GameObject playerObject;

    private void Start()
    {
        playerObject = this.gameObject;

        pMaxHealth = PlayerPrefs.GetInt("PlayerMaxHealth", pMaxHealth);
        pHealth = PlayerPrefs.GetInt("PlayerHealth", pHealth);
        pGuard = PlayerPrefs.GetInt("PlayerGuard", pGuard);

        pHealth = Mathf.Min(pHealth, pMaxHealth);
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

        if (pHealth > pMaxHealth) pHealth = pMaxHealth;

        if (pHealth <= 0)
        {
            Die();
        }

        SavePlayerStats();
    }

    public void IncreaseGuard(int guardValue)
    {
        pGuard += guardValue;
        //Debug.Log("가드증가 " + guardValue + ". 총 가드: " + pGuard);
        SavePlayerStats();
    }

    public void IncreaseHealth(int healthValue)
    {
        pHealth += healthValue;
        if (pHealth > pMaxHealth) pHealth = pMaxHealth;  // MaxHealth를 넘지 않도록

        SavePlayerStats();  // 체력 변경 시 저장
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

    private void SavePlayerStats()
    {
        PlayerPrefs.SetInt("PlayerMaxHealth", pMaxHealth);
        PlayerPrefs.SetInt("PlayerHealth", pHealth);
        PlayerPrefs.SetInt("PlayerGuard", pGuard);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SavePlayerStats();  // 게임 종료 시 데이터 저장
    }

}
