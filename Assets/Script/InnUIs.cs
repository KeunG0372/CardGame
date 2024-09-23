using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnUIs : MonoBehaviour
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

    public void RestNow()
    {
        Debug.Log("HP Restore");
    }
}
