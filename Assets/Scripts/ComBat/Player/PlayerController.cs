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
    [SerializeField] public static int pKarma = 0;

    public static bool karma = false;

    private bool isAttacking = false;
    [SerializeField] private float duration = 0.2f;

    GameObject playerObject;

    public static int plGold = 100;

    private void Start()
    {
        playerObject = this.gameObject;

        GameData data = FindObjectOfType<DataManager>().LoadGameData();

        if (data != null)
        {
            // 플레이어 상태를 저장된 값으로 복원
            pMaxHealth = data.playerMaxHP;
            pHealth = data.playerHP;
            pGuard = data.playerGP;
            pKarma = data.playerKarma;

            // 체력을 최대 체력 값으로 제한
            pHealth = Mathf.Min(pHealth, pMaxHealth);
        }
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
        // 현재 상태를 데이터 매니저를 통해 저장
        DataManager dataManager = FindObjectOfType<DataManager>();
        dataManager.SaveGameData(null, null, pHealth, pGuard, pKarma, null, null, null, false);
    }

    private void OnApplicationQuit()
    {
        SavePlayerStats();  // 게임 종료 시 데이터 저장
    }

    public void OnToggleChanged(bool value)
    {
        karma = value;
        Debug.Log($"Bool is now: {karma}");
    }

    public void KarmaEvent()
    {
        if (karma)
        {
            pKarma += 1;
        }
        else
        {
            pKarma -= 1;
        }

        SavePlayerStats();
    }

    public void KarmaEnd()
    {
        SavePlayerStats();
        Debug.Log(pKarma);
        SceneManager.LoadScene("MovingScene");
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
