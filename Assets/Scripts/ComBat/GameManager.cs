using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] NotificationPanel notificationPanel;
    [SerializeField] Text msHpText;
    [SerializeField] Text msGpText;
    [SerializeField] Text plHpText;
    [SerializeField] Text plGpText;
    [SerializeField] Text msAtkText;

    public PlayerController Player; // 플레이어 참조 추가

    [SerializeField] public int money;

    bool isMonsterTarget;


    private void Start()
    {
    }

    private void Update()
    {
#if UNITY_EDITOR
        InputCheatkey();
#endif
        plHpText.text = "HP  " + Player.pHealth.ToString();
        plGpText.text = "GP  " + Player.pGuard.ToString();
    }

    public void CheckAndAttackMonster(Card card)
    {
        foreach (var monster in MonsterManager.Inst.GetActiveMonsters())
        {
            if (card.GetComponent<Collider2D>().IsTouching(monster.GetComponent<Collider2D>()))
            {
                UseCard(card, monster);
                break; // 한 번만 공격하도록 루프 종료
            }
        }
    }

    public void UseCard(Card card, Monster monster)
    {
        monster.TakeDamage(card.damage);
        CardManager.Inst.RemoveCard(card);
        msHpText.text = "HP  " + monster.msHealth.ToString();
        msGpText.text = "GP  " + monster.msGuard.ToString();
        msAtkText.text = "Atk  " + monster.attackDamage.ToString();
    }

    void InputCheatkey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnManager.OnAddCard?.Invoke(this);
        }
        if (Input.GetKeyDown(KeyCode.F1))
            TurnManager.Inst.EndTurn();
    }


    public void Notification(string massage)
    {
        notificationPanel.Show(massage);
    }

}
