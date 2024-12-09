using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneController : MonoBehaviour
{
    public DataManager dataManager;
    public GameObject player;
    public GameObject[] randEvents;
    public GameObject[] randomEvents;

    private List<Vector3> tilePositions = new List<Vector3>();  // 타일 위치 리스트
    private List<int> tileTypes = new List<int>();  // 타일 타입 리스트

    public GameObject Inventoryobj;

    public static bool isBoss = false;


    void Start()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        Vector3 playerPosition = player.transform.position;
        int playerMaxHP = player.GetComponent<PlayerController>().pMaxHealth;
        int playerHP = player.GetComponent<PlayerController>().pHealth;
        int playerGP = player.GetComponent<PlayerController>().pGuard;
        //int playerKarma = player.GetComponent<PlayerController>().pKarma;
        var tilePositions = this.tilePositions;
        var tileTypes = this.tileTypes;

        Inventory_ inventory = Inventoryobj.GetComponent<Inventory_>();
        List<ItemProperty> slotItems = new List<ItemProperty>();
        if (inventory != null)
        {
            foreach (var slot in inventory.slots)
            {
                if (slot.item != null)
                {
                    slotItems.Add(slot.item);
                    Debug.Log($"Saving item: {slot.item.name}");
                }
                else
                {
                    slotItems.Add(new ItemProperty { name = "", price = 0, sprite = null, damage = 0 });
                    Debug.Log("Saving empty slot.");
                }
            }
        }

        dataManager.SaveGameData(playerPosition, playerMaxHP, playerHP, playerGP, 
            PlayerController.pKarma, tilePositions, tileTypes, slotItems, false);
    }

    public void LoadGame()
    {
        GameData data = dataManager.LoadGameData();

        if (data != null)
        {
            player.transform.position = data.playerPosition;
            player.GetComponent<PlayerController>().pMaxHealth = data.playerMaxHP;
            player.GetComponent<PlayerController>().pHealth = data.playerHP;
            player.GetComponent<PlayerController>().pGuard = data.playerGP;

            tilePositions = new List<Vector3>(data.tilePositions);
            tileTypes = new List<int>(data.tileTypes);

            Inventory_ inventory = Inventoryobj.GetComponent<Inventory_>();

            if (inventory != null)
            {
                Debug.Log($"Restoring {data.slots.Count} items in inventory.");
                for (int i = 0; i < inventory.slots.Count && i < data.slots.Count; i++)
                {
                    inventory.slots[i].Setltem(data.slots[i]);
                    Debug.Log($"Restored item: {data.slots[i]?.name ?? "Empty"}");
                }
            }

            if (inventory == null)
            {
                inventory = Inventoryobj.GetComponent<Inventory_>();
            }

            if (data.init)
            {
                InitializeTileDataFromPrefabs();
                data.init = false;
                dataManager.SaveGameData(null, null, null, null, null, null, null, null, false);
            }
            else
            {
                ClearExistingTiles();
                RestoreTiles(data);
            }
        }
    }

    private void RestoreTiles(GameData data)
    {
        for (int i = 0; i < data.tilePositions.Count; i++)
        {
            Vector3 position = data.tilePositions[i];
            int tileIndex = data.tileTypes[i];

            if (tileIndex >= 0 && tileIndex < randomEvents.Length)
            {
                Instantiate(randomEvents[tileIndex], position, Quaternion.identity);
            }
        }

        Debug.Log("All tiles restored.");
    }

    void InitializeTileDataFromPrefabs()
    {
        tilePositions.Clear();
        tileTypes.Clear();

        foreach (GameObject prefab in randEvents)
        {
            Vector3 position = prefab.transform.position;

            int random = Random.Range(0, randomEvents.Length);
            GameObject randomPrefab = randomEvents[random];

            Instantiate(randomPrefab, position, Quaternion.identity);
            Destroy(prefab);

            tilePositions.Add(position);
            tileTypes.Add(random);
        }

        Debug.Log($"Initialized tiles: {tilePositions.Count}");
        SaveGame();
    }

    void ClearExistingTiles()
    {
        var existingTiles = GameObject.FindGameObjectsWithTag("Random");
        foreach (var tile in existingTiles)
        {
            Destroy(tile);
        }
    }



    private void OnApplicationQuit()
    {
        SaveGame();  // 게임 종료 시 데이터 저장
    }

    public void GoToCombatScene()
    {
        if (DiceRoll.isDiceActive)
        {
            DiceRoll.isDiceActive = false;
            SaveGame();
            SceneManager.LoadScene("CombatScene");
        }
    }


    public void GoToKarmaScene()
    {
        if (DiceRoll.isDiceActive)
        {
            DiceRoll.isDiceActive = false;
            SaveGame();
            SceneManager.LoadScene("KarmaScene");
        }
    }
}
