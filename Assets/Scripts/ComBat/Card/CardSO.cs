using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInfo
{
    public string name;     //ī���̸�
    public int damage;      //���ݷ�
    public int guard;       //����
    public int buff;        //���ݷ� ����
    //public Sprite sprite; //�̹��� ���� ����
    public float percent;   //����Ȯ��
    public string info;     //ī�������ؽ�Ʈ(����)
    public bool isAttack;   //����ī������
    public int index;       //ī�� �ε��� ��ȣ
    public bool pHave;      //�÷��̾ ������ �ִ��� (����� �� �ִ���)
}

[CreateAssetMenu(fileName = "CardSO", menuName = "Scriptable Object/CardSO")]
public class CardSO : ScriptableObject
{
    public CardInfo[] cards;
}
