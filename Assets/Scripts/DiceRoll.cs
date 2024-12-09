using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public GameObject DiceCanvas;
    public static bool isroll = false;
    public static bool isDiceActive = false;

    // Start is called before the first frame update
    void Start()
    {
        DiceCanvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickActions()
    {
        DiceCanvas.SetActive(true);
        isroll = true;
        isDiceActive = true;
    }
}
