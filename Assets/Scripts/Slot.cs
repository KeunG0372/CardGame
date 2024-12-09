using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public UnityEngine.UI.Image image;
    public UnityEngine.UI.Button sellB;

    private void Awake()
    {
        SetSellBInteractable(false);
    }
    void SetSellBInteractable(bool b)
    {
        if (sellB != null)
        {
            sellB.interactable = b;
        }
    }
    public void Setltem(ItemProperty item)
    {
        this.item = item;
        if (item == null)
        {
            image.enabled = false;
            image.color = new Color(1, 1, 1, 0);
            SetSellBInteractable(false);
            gameObject.name = "Empty";
        }
        else
        {
            image.enabled = true;
            image.color = new Color(1, 1, 1, 1);
            SetSellBInteractable(true);
            gameObject.name = item.name;
            image.sprite = item.sprite;
        }
    }
    public void OnClickSellB()
    {
        Setltem(null);
    }

    public void SetVisible(bool visible)
    {
        var image = GetComponent<Image>();
        if (visible)
        {
            image.color = new Color(1, 1, 1, 1);
        }
        else
        {
            image.color = new Color(1, 1, 1, 0);
        }
    }
}
