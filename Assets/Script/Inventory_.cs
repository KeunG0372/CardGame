using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_ : MonoBehaviour
{
    public Transform rootSlot;
    private List<Slot> slots;
    public Store store;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<Slot>();
        int slotCnt=rootSlot.childCount;
        for(int i=0; i<slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();
            slots.Add(slot);
        }
        store.onSlotClick += BuyItem;
    }
    void BuyItem(ItemProperty item)
    {
        var emptySlot = slots.Find(t => { return t.item == null || t.item.name == string.Empty; });   
        if (emptySlot != null)
        {
            emptySlot.Setltem(item);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
