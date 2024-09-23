using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }

    void Awake() => Inst = this;

    [SerializeField] CardSO cardSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] public List<Card> cards;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform cardLeft;
    [SerializeField] Transform cardRight;
    [SerializeField] ECardState eCardState;


    List<CardInfo> cardBuffer;
    public Card selectCard;
    public bool isCardDrag;
    bool onCardArea;
    enum ECardState { Nothing, CanMouseOver, CanMouseDrag }


    public CardInfo PopCard()
    {
        if (cardBuffer.Count == 0)
            SetupCardBuffer();

        CardInfo card = cardBuffer[0];
        cardBuffer.RemoveAt(0);
        return card;
    }

    void SetupCardBuffer()
    {
        cardBuffer = new List<CardInfo>(100);
        for (int i = 0; i < cardSO.cards.Length; i++) 
        {
            CardInfo card = cardSO.cards[i];
            for (int j = 0; j < card.percent; j++)
                if (card.pHave == true)
                    cardBuffer.Add(card);
        }

        for (int i = 0; i < cardBuffer.Count; i++)
        {
            int rand = Random.Range(0, cardBuffer.Count);
            CardInfo temp = cardBuffer[i];
            cardBuffer[i] = cardBuffer[rand];
            cardBuffer[rand] = temp;
        }
    }

    private void Start()
    {
        SetupCardBuffer();
        TurnManager.OnAddCard += AddCard;
    }

    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
    }

    private void Update()
    {
        if (isCardDrag)
        {
            CardDrag();
        }

        DetectCardArea();
        SetEcardState();
    }

    

    void AddCard(bool tmp)
    {
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopCard());
        cards.Add(card);

        SetOriginOrder();
        CardAlignment();
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        Destroy(card.gameObject);
    }

    public void MoveAllCardsToGraveyard()
    {
        foreach (var card in cards)
        {
            MoveCardToGraveyard(card);
        }
        cards.Clear();
    }

    void MoveCardToGraveyard(Card card)
    {
        Vector3 endPos = cardSpawnPoint.position;
        Quaternion endRot = Utils.QI;
        Vector3 endScale = Vector3.one * 0.6f;
        float duration = 0.7f;

        card.transform.DOMove(endPos, duration).SetEase(Ease.InOutQuad);
        card.transform.DORotateQuaternion(endRot, duration).SetEase(Ease.InOutQuad);
        card.transform.DOScale(endScale, duration).SetEase(Ease.InOutQuad).OnComplete(() => {
            Destroy(card.gameObject);
        });
    }

    void SetOriginOrder()
    {
        int count = cards.Count;
        for (int i = 0; i < count; i++) 
        { 
            var targetCard = cards[i];
            targetCard.GetComponent<Order>().SetOrigininOrder(i);
        }
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(cardLeft, cardRight, cards.Count, 0.5f, Vector3.one * 0.6f);

        var targetCards = cards;
        for (int i = 0; i < targetCards.Count; i++) 
        { 
            var targetCard = targetCards[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.4f, 0.6f }; break;
            case 3: objLerps = new float[] { 0.3f, 0.5f, 0.7f }; break;
            case 4: objLerps = new float[] { 0.2f, 0.4f, 0.6f, 0.8f}; break;
            case 5: objLerps = new float[] { 0.1f, 0.3f, 0.5f, 0.7f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++) 
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount >= 1) 
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    #region MyCard

    public void CardMouseOver(Card card)
    {
        if (eCardState == ECardState.Nothing)
            return;

        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isCardDrag = true;
    }

    public void CardMouseUp()
    {
        isCardDrag = false;

        if (eCardState != ECardState.CanMouseDrag)
            return;

        if (selectCard != null && selectCard.cardInfo.isAttack)
        {
            DetectCardMonsterCollision();
        }
        else if (selectCard != null && !selectCard.cardInfo.isAttack)
        {
            if (!onCardArea)
            {
                GameManager.Inst.Player.IncreaseGuard(selectCard.cardInfo.guard);
                RemoveCard(selectCard);
            }
        }
    }

    private void CardDrag()
    {
        if (!onCardArea)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }

    
    void DetectCardMonsterCollision()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(selectCard.transform.position, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                Monster monster = hit.collider.GetComponent<Monster>();
                if (monster != null)
                {
                    GameManager.Inst.Player.IncreaseGuard(selectCard.cardInfo.guard);
                    GameManager.Inst.UseCard(selectCard, monster);

                    break;
                }
            }
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("CardArea");
        onCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    void EnlargeCard(bool isEnlarge, Card card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -3f, -5f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 1f), false);
        }
        else 
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

    private void SetEcardState()
    {
        if (TurnManager.Inst.isLoading)
            eCardState = ECardState.Nothing;
        
        else if (!TurnManager.Inst.myTurn)
            eCardState = ECardState.CanMouseOver;

        else if (TurnManager.Inst.myTurn)
            eCardState = ECardState.CanMouseDrag;
    }

    #endregion
}
