using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;  // �÷��̾� ��ġ
    public List<Vector3> tilePositions;  // Ÿ�� ��ġ��
    public List<int> tileTypes;  // Ÿ���� Ÿ��(�ε���)
    public List<Vector3> disabledBattleTiles;   // ��Ȱ��ȭ�� ���� Ÿ���� ��ġ
}

public class DataManager : MonoBehaviour
{
    private string savePath;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "gameData.json");
    }

    public void SaveGameData(Vector3 playerPosition, List<Vector3> tilePositions, List<int> tileTypes, List<Vector3> disabledBattleTiles)
    {
        GameData data = new GameData
        {
            playerPosition = playerPosition,
            tilePositions = tilePositions,
            tileTypes = tileTypes,
            disabledBattleTiles = disabledBattleTiles
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public GameData LoadGameData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        return null;
    }

    public void ResetGameData(Vector3 initialPosition)
    {
        // �ʱ� ��ġ�� �⺻ Ÿ�� �����ͷ� �ʱ�ȭ
        GameData data = new GameData
        {
            playerPosition = initialPosition,
            tilePositions = new List<Vector3>(),  // �ʱ� ������ Ÿ�� ��ġ
            tileTypes = new List<int>()  // �ʱ� ������ Ÿ�� Ÿ��
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
}
