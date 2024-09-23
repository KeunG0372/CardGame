using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //이건 상점 UI
    public GameObject ShopUI;

    //이건 휴식지 UI
    public GameObject InnUI;


    // Start is called before the first frame update
    void Start()
    {
        ShopUI.SetActive(false);
        InnUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(DiceRoll.isroll == true)
        {
            PlayerMoving();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "noGoZone")
        {
            //Debug.Log("나 닿음");
            transform.position = new Vector3(6.0f,0,0);
        }

        else if(collision.tag == "Battle")
        {
            SceneManager.LoadScene("ComBatScene");
            Debug.Log("나 닿음");
        }

        else if(collision.tag == "Shop")
        {
            ShopUI.SetActive(true);
        }

        else if (collision.tag == "Inn")
        {
            InnUI.SetActive(true);
        }

        else if (collision.tag == "Karma")
        {
            //ShopUI.SetActive(true);
        }

        else if (collision.tag == "MainEvent")
        {
            //ShopUI.SetActive(true);
        }

        else if (collision.tag == "BossBattle")
        {
            SceneManager.LoadScene("ComBatScene");
        }
    }
}
