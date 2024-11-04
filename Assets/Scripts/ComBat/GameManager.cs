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

    [SerializeField] public int pendingBuffDamage = 0;


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
        if (card.cardInfo.isAttack)
        {
            int totalDamage = card.CurrentDamage;
            totalDamage -= card.CurrentBuff;    // 버프 데미지가 계속 포함되는 버그로 포함시킴
            monster.TakeDamage(totalDamage);

            msHpText.text = "HP  " + monster.msHealth.ToString();
            msGpText.text = "GP  " + monster.msGuard.ToString();
            msAtkText.text = "Atk  " + monster.attackDamage.ToString();

            Player.Attack(monster.transform.position);
        }

        if (card.cardInfo.isBuff)
        {
            pendingBuffDamage += card.CurrentBuff;
        }

        CardManager.Inst.RemoveCard(card);
    }

    public void ApplyBuffToAllCards(int buffIncrease)
    {
        CardManager.Inst.ApplyBuffToAllCards(buffIncrease);
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
