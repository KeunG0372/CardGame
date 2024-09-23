using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    private void Awake() => Inst = this;


    [Header("Develop")]
    [SerializeField][Tooltip("시작 턴 모드 정하기")] EturnMode eTurnMode;
    [SerializeField][Tooltip("시작 카드 개수 정하기")] int startCardCount;

    [Header("Properties")]
    public bool isLoading;  // 끝나면 카드 엔티티 클릭 방지
    public bool myTurn;

    [SerializeField] Button turnEndBtn;

    enum EturnMode { Random, My, Other }
    WaitForSeconds delay = new WaitForSeconds(0.1f);

    public static Action<bool> OnAddCard;

    private void Start()
    {
        GameSetUp();
        StartCoroutine(StartGameCo());

        turnEndBtn.onClick.AddListener(EndTurn);
    }

    private void Update()
    {
    }

    void GameSetUp()
    {
        switch (eTurnMode)
        {
            case EturnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;
            case EturnMode.My:
                myTurn = true;
                break;
            case EturnMode.Other:
                myTurn = false;
                break;
        }
    }

    public IEnumerator StartGameCo()
    {
        //GameSetUp();

        for (int i = 0; i < startCardCount; i++)
        {
            yield return delay;
            OnAddCard?.Invoke(true);
        }
        StartCoroutine(StartTurnCo());
    }

    IEnumerator StartTurnCo()
    {
        isLoading = true;

        if (myTurn)
            GameManager.Inst.Notification("나의 턴");

        yield return delay;
        //OnAddCard?.Invoke(myTurn);    //시작때마다 카드주기 어떻게 할지 구상중 
        yield return delay;
        isLoading = false;
    }

    public void EndTurn()
    {
        if (myTurn)
        {
            myTurn = false;
            StartCoroutine(EnemyTurnCo());

            GameManager.Inst.Notification("적의 턴");
        }
    }

    IEnumerator EnemyTurnCo()
    {
        yield return new WaitForSeconds(1f);

        foreach (var monster in MonsterManager.Inst.GetActiveMonsters())
        {
            GameManager.Inst.Player.TakeDamage(monster.attackDamage);
        }

        CardManager.Inst.MoveAllCardsToGraveyard();

        yield return new WaitForSeconds(1f);

        myTurn = true;
        StartCoroutine(StartGameCo());
    }

}
