using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInfo
{
    public string name;     //카드이름
    public int damage;      //공격력
    public int guard;       //가드
    public int buff;        //공격력 증가
    //public Sprite sprite; //이미지 아직 없음
    public float percent;   //등장확률
    public string info;     //카드정보텍스트(설명)
    public bool isAttack;   //공격카드인지
    public int index;       //카드 인덱스 번호
    public bool pHave;      //플레이어가 가지고 있는지 (사용할 수 있는지)
}

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public CardInfo[] cards;
}
