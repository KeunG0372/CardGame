using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSceneController : MonoBehaviour
{
    public DataManager dataManager;
    public GameObject player;
    public GameObject[] randEvents;
    public GameObject[] randomEvents;
    private List<Vector3> tilePositions = new List<Vector3>();  // Ÿ�� ��ġ ����Ʈ
    private List<int> tileTypes = new List<int>();  // Ÿ�� Ÿ�� ����Ʈ
    private List<Vector3> disabledBattleTiles = new List<Vector3>();  // ��Ȱ��ȭ�� ���� Ÿ�� ��ġ

    void Start()
    {
        LoadGame();
    }

    void LoadGame()
    {
        GameData data = dataManager.LoadGameData();
        if (data != null)
        {
            player.transform.position = data.playerPosition;

            for (int i = 0; i < data.tilePositions.Count; i++)
            {
                Vector3 position = data.tilePositions[i];
                int tileIndex = data.tileTypes[i];
                GameObject tile = Instantiate(randEvents[tileIndex], position, Quaternion.identity);

                if (GlobalGameManager.Instance.disabledBattleTilePositions.Contains(position))
                {
                    tile.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
        else
        {
            InitializeTileDataFromPrefabs();
        }
    }

    public void SaveGame()
    {
        Vector3 playerPosition = player.transform.position;
        dataManager.SaveGameData(playerPosition, tilePositions, tileTypes, disabledBattleTiles);
    }

    public void DisableBattleTile(Vector3 position)
    {
        GlobalGameManager.Instance.DisableBattleTile(position);
    }

    void InitializeTileDataFromPrefabs()
    {
        tilePositions.Clear();
        tileTypes.Clear();

        for (int i = 0; i < randEvents.Length; i++)
        {
            GameObject prefab = randEvents[i];
            Vector3 position = prefab.transform.position;

            // �̺�Ʈ Ÿ�� ����
            Instantiate(prefab, position, Quaternion.identity);

            // ��ġ�� Ÿ�� ����
            tilePositions.Add(position);
            tileTypes.Add(i);
        }

        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();  // ���� ���� �� ������ ����
    }

    public void GoToCombatScene()
    {
        SaveGame();
        SceneManager.LoadScene("CombatScene");
    }
}
