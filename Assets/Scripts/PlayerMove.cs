using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // ���� �� �޽��� UI
    public GameObject ShopUI;
    public GameObject InnUI;

    private MapSceneController mapSceneController;

    private HashSet<GameObject> collidedTiles = new HashSet<GameObject>();

    public bool isShopLoaded = false;


    void Start()
    // Start is called before the first frame update
    {
        ShopUI.SetActive(false);
        InnUI.SetActive(false);

        mapSceneController = FindObjectOfType<MapSceneController>();
        if (mapSceneController == null)
        {
            Debug.LogError("MapSceneController�� ���� ����.");
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
                mapSceneController.GoToCombatScene();
            }
        }

        else if (collision.tag == "Shop")
        {
            if (!isShopLoaded)
            {
                mapSceneController.LoadGame();
                ShopUI.SetActive(true);
                isShopLoaded = true;
            }
        }

        else if (collision.tag == "Inn")
        {
            InnUI.SetActive(true);
        }

        else if (collision.tag == "Karma")
        {
            if (mapSceneController != null)
            {
                mapSceneController.GoToKarmaScene();
            }
        }

        else if (collision.tag == "MainEvent")
        {
            if (mapSceneController != null)
            {
                mapSceneController.GoToKarmaScene();
            }
        }

        else if (collision.tag == "BossBattle")
        {
            if (mapSceneController != null)
            {
                MapSceneController.isBoss = true;
                mapSceneController.GoToCombatScene();
            }
        }
    }
}
