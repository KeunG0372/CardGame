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

    public int CurrentDamage => currentDamage;
    public int CurrentGuard => currentGuard;
    public int CurrentBuff => currentBuff;

    private int currentDamage;
    private int currentGuard;
    private int currentBuff;
    public PRS originPRS;

    //public int damage;
    //public int guard;
    //public int buff;
    public bool isAttack;

    private void Start()
    {
        // ī�尡 ������ �� CardManager�� ���
        CardManager.Inst.RegisterCard(this);
    }

    private void OnDestroy()
    {
        // ī�尡 �ı��� �� CardManager���� ����
        CardManager.Inst.UnregisterCard(this);
    }

    public void UpdateInfoText()
    {
        string displayText = "";

        if (cardInfo.info.Contains("damage"))
        {
            displayText += $"Damage {currentDamage} ";
        }
        if (cardInfo.info.Contains("guard"))
        {
            displayText += $"Guard {currentGuard} ";
        }
        if (cardInfo.info.Contains("buff"))
        {
            displayText += $"Buff {currentBuff} ";
        }

        infoTxt.text = displayText.Trim();
    }

    public void UpdateValues(int damageIncrease, int guardIncrease, int buffIncrease)
    {
        if (cardInfo.info.Contains("damage"))
        {
            currentDamage += damageIncrease;
        }
        if (cardInfo.info.Contains("guard"))
        {
            currentGuard += guardIncrease;
        }
        if (cardInfo.info.Contains("buff"))
        {
            currentBuff += buffIncrease;
        }

        UpdateInfoText();
    }

    public void IncreaseDamage(int amount)
    {
        currentDamage += amount;
        UpdateInfoText();
    }

    public void Setup(CardInfo cardInfo)
    {
        //image.sprite = this.cardInfo.sprite;                //�ӽ� ���� ������ �ƹ��͵� ����
        //infoTxt.text = this.cardInfo.info;                  //ī�� ���� �ؽ�Ʈ�� �ҷ�����
        //damageTxt.text = this.cardInfo.damage.ToString();   //������ �ؽ�Ʈ�� �ҷ�����
        //damage = this.cardInfo.damage;
        //guard = this.cardInfo.guard;
        //buff = this.cardInfo.buff;

        this.cardInfo = cardInfo;

        isAttack = this.cardInfo.isAttack;

        currentDamage = cardInfo.damage;
        currentGuard = cardInfo.guard;
        currentBuff = cardInfo.buff;

        UpdateInfoText();
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
