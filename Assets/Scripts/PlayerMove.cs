using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // 상점 및 휴식지 UI
    public GameObject ShopUI;
    public GameObject InnUI;

    private MapSceneController mapSceneController;

    private HashSet<GameObject> collidedTiles = new HashSet<GameObject>();


    void Start()
    // Start is called before the first frame update
    {
        ShopUI.SetActive(false);
        InnUI.SetActive(false);

        mapSceneController = FindObjectOfType<MapSceneController>();
        if (mapSceneController == null)
        {
            Debug.LogError("MapSceneController가 씬에 없음.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "noGoZone")
        {
            transform.position = new Vector3(6.0f, 0, 0);
        }

        else if (collision.tag == "Battle")
        {
            if (mapSceneController != null)
            {
                mapSceneController.DisableBattleTile(collision.transform.position);
                mapSceneController.GoToCombatScene();
            }
        }

        else if (collision.tag == "Shop")
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
            if (mapSceneController != null)
            {
                mapSceneController.GoToCombatScene();
            }
        }
    }
}
