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
            // �÷��̾� ���¸� ����� ������ ����
            pMaxHealth = data.playerMaxHP;
            pHealth = data.playerHP;
            pGuard = data.playerGP;
            pKarma = data.playerKarma;

            // ü���� �ִ� ü�� ������ ����
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
        //Debug.Log("�������� " + guardValue + ". �� ����: " + pGuard);
        SavePlayerStats();
    }

    public void IncreaseHealth(int healthValue)
    {
        pHealth += healthValue;
        if (pHealth > pMaxHealth) pHealth = pMaxHealth;  // MaxHealth�� ���� �ʵ���

        SavePlayerStats();  // ü�� ���� �� ����
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

        // �÷��̾ ���� �������� �̵�
        Vector3 targetPosition = Vector3.Lerp(originalPosition, monsterPosition, 0.15f);
        transform.DOMove(targetPosition, duration);
        yield return new WaitForSeconds(duration);

        // ���� ��ġ�� �̵�
        transform.DOMove(originalPosition, duration);
        yield return new WaitForSeconds(duration);

        isAttacking = false;
    }

    private void SavePlayerStats()
    {
        // ���� ���¸� ������ �Ŵ����� ���� ����
        DataManager dataManager = FindObjectOfType<DataManager>();
        dataManager.SaveGameData(null, null, pHealth, pGuard, pKarma, null, null, null, false);
    }

    private void OnApplicationQuit()
    {
        SavePlayerStats();  // ���� ���� �� ������ ����
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
