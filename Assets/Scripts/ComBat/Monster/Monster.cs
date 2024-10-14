using DG.Tweening;
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

    GameObject monsterObject;
    [SerializeField] float duration = 0.2f;

    private void Start()
    {
        monsterObject = this.gameObject;
    }

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

    public void Attack(Vector3 playerPosition)
    {
        StartCoroutine(PerformAttack(playerPosition));
    }

    private IEnumerator PerformAttack(Vector3 playerPosition)
    {

        Vector3 originalPosition = transform.position;

        // ���͸� �÷��̾� �������� �̵�
        Vector3 targetPosition = Vector3.Lerp(originalPosition, playerPosition, 0.15f);
        transform.DOMove(targetPosition, duration);
        yield return new WaitForSeconds(duration);

        // ���� ��ġ�� �̵�
        transform.DOMove(originalPosition, duration);
        yield return new WaitForSeconds(duration);

    }
}
