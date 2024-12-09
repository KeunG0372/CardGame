using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public Transform slotRoot;
    public ItemBuffer itemBuffer;
    private List<Slot> slots;
    public System.Action<ItemProperty> onSlotClick;
    // Start is called before the first frame update
    void Start()
    {   slots=new List<Slot>();
        int slotCnt=slotRoot.childCount;
        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            if (i < itemBuffer.items.Count)
            {
                slot.Setltem(itemBuffer.items[i]);

                var text = slot.transform.GetChild(0).GetComponent<Text>();
                text.text = slot.item.price.ToString();
            }
            else
            {
                slot.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
            slots.Add(slot);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickSlot(Slot slot)
    {
        if (onSlotClick != null)
        {
            onSlotClick(slot.item);
        }
    }
}
