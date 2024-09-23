using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] NotificationPanel notificationPanel;
    [SerializeField] Text msText;
    [SerializeField] Text plText;
    
    public PlayerController Player; // �÷��̾� ���� �߰�

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
        plText.text = Player.pHealth.ToString();
    }

    public void CheckAndAttackMonster(Card card)
    {
        foreach (var monster in MonsterManager.Inst.GetActiveMonsters())
        {
            if (card.GetComponent<Collider2D>().IsTouching(monster.GetComponent<Collider2D>()))
            {
                UseCard(card, monster);
                break; // �� ���� �����ϵ��� ���� ����
            }
        }
    }

    public void UseCard(Card card, Monster monster)
    {
        monster.TakeDamage(card.damage);
        CardManager.Inst.RemoveCard(card);
        msText.text = monster.msHealth.ToString();
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
