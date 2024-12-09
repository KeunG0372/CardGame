using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_ : MonoBehaviour
{
    public Transform rootSlot;
    public List<Slot> slots;
    public Store store;

    public Text goldTxt;

    DataManager datamanager;

    // Start is called before the first frame update
    void Start()
    {
        datamanager = FindObjectOfType<DataManager>();

        slots = new List<Slot>();
        int slotCnt = rootSlot.childCount;
        for (int i = 0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();
            slots.Add(slot);
        }

        //Debug.Log($"Initialized {slots.Count} slots in inventory.");
        store.onSlotClick += BuyItem;
        goldTxt.text = "Gold : " + PlayerController.plGold.ToString();
    }

    void BuyItem(ItemProperty item)
    {
        int plGold = PlayerController.plGold;
        if (plGold > item.price)
        {
            var emptySlot = slots.Find(t => { return t.item == null || t.item.name == string.Empty; });
            if (emptySlot != null)
            {
                emptySlot.Setltem(item);
                PlayerController.plGold -= item.price;
                goldTxt.text = "Gold : " + PlayerController.plGold.ToString();
                SaveInventory();
            }
        }
    }

    public void SaveInventory()
    {
        List<ItemProperty> slotItems = new List<ItemProperty>();
        foreach (var slot in slots)
        {
            if (slot.item != null)
            {
                slotItems.Add(slot.item);
                //Debug.Log($"Saving item: {slot.item.name}");
            }
            else
            {
                slotItems.Add(new ItemProperty { name = "", price = 0, sprite = null, damage = 0 });
                //Debug.Log("Saving empty slot.");
            }
        }

        datamanager.SaveGameData(null, null, null, null, null, null, null, slotItems, null);
    }

    public void LoadInventory()
    {
        GameData data = datamanager.LoadGameData();
        if (data != null && data.slots != null)
        {
            for (int i = 0; i < slots.Count && i < data.slots.Count; i++)
            {
                slots[i].Setltem(data.slots[i]);
            }
        }
    }
}
