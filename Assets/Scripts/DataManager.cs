using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;  // 플레이어 위치
    public List<Vector3> tilePositions;  // 타일 위치들
    public List<int> tileTypes;  // 타일의 타입(인덱스)
    public List<Vector3> disabledBattleTiles;   // 비활성화된 전투 타일의 위치
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
        // 초기 위치와 기본 타일 데이터로 초기화
        GameData data = new GameData
        {
            playerPosition = initialPosition,
            tilePositions = new List<Vector3>(),  // 초기 상태의 타일 위치
            tileTypes = new List<int>()  // 초기 상태의 타일 타입
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
}
