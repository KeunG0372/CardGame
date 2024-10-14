using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer image;
    [SerializeField] TextMeshPro infoTxt;
    [SerializeField] TextMeshPro damageTxt;

    public CardInfo cardInfo;
    public PRS originPRS;

    public int damage;
    public int guard;
    public int buff;
    public bool isAttack;


    public void Setup(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;

        //image.sprite = this.cardInfo.sprite;                //�ӽ� ���� ������ �ƹ��͵� ����
        infoTxt.text = this.cardInfo.info;                  //ī�� ���� �ؽ�Ʈ�� �ҷ�����
        damageTxt.text = this.cardInfo.damage.ToString();   //������ �ؽ�Ʈ�� �ҷ�����

        damage = this.cardInfo.damage;
        guard = this.cardInfo.guard;
        isAttack = this.cardInfo.isAttack;
        buff = this.cardInfo.buff;
    }

    private void OnMouseOver()
    {
        CardManager.Inst.CardMouseOver(this);
    }

    private void OnMouseExit()
    {
        CardManager.Inst.CardMouseExit(this);
    }

    private void OnMouseDown()
    {
        CardManager.Inst.CardMouseDown();
    }

    private void OnMouseUp()
    {
        CardManager.Inst.CardMouseUp();
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotwennTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotwennTime);
            transform.DORotateQuaternion(prs.rot, dotwennTime);
            transform.DOScale(prs.scale, dotwennTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
