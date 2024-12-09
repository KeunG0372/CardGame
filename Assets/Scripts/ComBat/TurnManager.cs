using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    GameObject playerObject;

    private void Start()
    {
        GameSetUp();
        StartCoroutine(StartGameCo());

        turnEndBtn.onClick.AddListener(EndTurn);

        playerObject = GameObject.Find("Player");

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
            GameManager.Inst.Notification("My Turn");

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

            // 남은 몬스터가 있는지 확인 후 적 턴으로 넘어가거나 씬 이동
            if (MonsterManager.Inst.GetActiveMonsters().Count > 0)
            {
                StartCoroutine(EnemyTurnCo());
                GameManager.Inst.Notification("Enemy Turn");
            }
            else
            {
                GameManager.Inst.Notification("VicTory!");

                if (!MapSceneController.isBoss)
                    StartCoroutine(SceneMove());  // 모든 몬스터가 처치된 경우 씬 이동
                else
                    StartCoroutine(EndingScene());
            }
        }
    }

    public void GameEnd()
    {
        StartCoroutine(SceneMove());
    }

    private IEnumerator EndingScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("EndingScene");
    }

    IEnumerator EnemyTurnCo()
    {
        yield return new WaitForSeconds(1.5f);

        foreach (var monster in MonsterManager.Inst.GetActiveMonsters())
        {
            GameManager.Inst.Player.TakeDamage(monster.attackDamage);

            monster.Attack(playerObject.transform.position);

            yield return new WaitForSeconds(0.5f);


        }

        CardManager.Inst.MoveAllCardsToGraveyard();

        yield return new WaitForSeconds(0.5f);

        myTurn = true;
        StartCoroutine(StartGameCo());
    }

    private IEnumerator SceneMove()
    {
        DataManager dataManager = FindObjectOfType<DataManager>();
        MapSceneController mapSceneController = FindObjectOfType<MapSceneController>();

        mapSceneController?.SaveGame();

        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MovingScene");
    }

}
