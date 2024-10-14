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

    [SerializeField] int buffDamage = 0;
    [SerializeField] int totalDamage;


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

    public void UseCard(Card card, Monster monster)
    {

        monster.TakeDamage(totalDamage);
        CardManager.Inst.RemoveCard(card);
        msHpText.text = "HP  " + monster.msHealth.ToString();
        msGpText.text = "GP  " + monster.msGuard.ToString();
        msAtkText.text = "Atk  " + monster.attackDamage.ToString();

        if (card.isAttack)
        {
            Player.Attack(monster.transform.position);
        }

    }

    public void IncreaseBuff(Card card)
    {
        buffDamage += card.buff;

        totalDamage = card.damage + buffDamage;
    }

    public void EndBuff()
    {
        buffDamage = 0;
        totalDamage = 0;
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
