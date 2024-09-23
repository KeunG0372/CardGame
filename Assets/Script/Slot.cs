using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(sellB != null)
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
            SetSellBInteractable(false );
            gameObject.name="Empty";
        }
        else
        {
            image.enabled = true;
            SetSellBInteractable(true);
            gameObject.name=item.name;
            image.sprite=item.sprite;
        }
    }
    public void OnClickSellB()
    {
        Setltem(null);  
    }
}
