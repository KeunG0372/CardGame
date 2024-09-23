using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopExit : MonoBehaviour
{
    public GameObject offPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NowOut()
    {
        offPanel.SetActive(false);
    }
}
