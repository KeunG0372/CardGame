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
    [SerializeField][Tooltip("���� �� ��� ���ϱ�")] EturnMode eTurnMode;
    [SerializeField][Tooltip("���� ī�� ���� ���ϱ�")] int startCardCount;

    [Header("Properties")]
    public bool isLoading;  // ������ ī�� ��ƼƼ Ŭ�� ����
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
        //OnAddCard?.Invoke(myTurn);    //���۶����� ī���ֱ� ��� ���� ������ 
        yield return delay;
        isLoading = false;
    }

    public void EndTurn()
    {
        if (myTurn)
        {
            myTurn = false;

            // ���� ���Ͱ� �ִ��� Ȯ�� �� �� ������ �Ѿ�ų� �� �̵�
            if (MonsterManager.Inst.GetActiveMonsters().Count > 0)
            {
                StartCoroutine(EnemyTurnCo());
                GameManager.Inst.Notification("Enemy Turn");
            }
            else
            {
                GameManager.Inst.Notification("VicTory!");

                if (!MapSceneController.isBoss)
                    StartCoroutine(SceneMove());  // ��� ���Ͱ� óġ�� ��� �� �̵�
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
