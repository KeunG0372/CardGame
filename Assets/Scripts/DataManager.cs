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
    public int playerMaxHP;
    public int playerHP;  // 플레이어 체력
    public int playerGP;  // 플레이어 가드 포인트
    public int playerKarma;
    public List<ItemProperty> slots = new List<ItemProperty>();
    public bool init = false;
}

public class DataManager : MonoBehaviour
{
    private string savePath;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "gameData.json");
    }

    public void SaveGameData(Vector3? playerPosition, int? playerMaxHealth, int? playerHP, int? playerGP, int? playerKarma,
                         List<Vector3> tilePositions, List<int> tileTypes, List<ItemProperty> slotItems, bool? init)
    {
        GameData data = LoadGameData() ?? new GameData();  // 기존 데이터를 불러오거나 새로 생성

        // null 값이 아닌 경우에만 데이터를 업데이트
        if (playerPosition.HasValue) data.playerPosition = playerPosition.Value;
        if (playerMaxHealth.HasValue) data.playerMaxHP = playerMaxHealth.Value;
        if (playerHP.HasValue) data.playerHP = playerHP.Value;
        if (playerGP.HasValue) data.playerGP = playerGP.Value;
        if (playerKarma.HasValue) data.playerKarma = playerKarma.Value;

        if (tilePositions != null) data.tilePositions = tilePositions;
        if (tileTypes != null) data.tileTypes = tileTypes;

        if (slotItems != null) data.slots = slotItems;

        if (init.HasValue) data.init = init.Value;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        //Debug.Log(data);
    }

    public GameData LoadGameData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);

#pragma warning disable CS0472 // 이 형식의 값은 'null'과 같을 수 없으므로 식의 결과가 항상 동일합니다.
            if (data.init == null)
            {
                data.init = false;
            }
#pragma warning restore CS0472 // 이 형식의 값은 'null'과 같을 수 없으므로 식의 결과가 항상 동일합니다.

            return data;
        }
        return null;
    }

    public void ResetGameData(Vector3 initialPosition, int defaultMaxHP, int defaultHP, int defaultGP,
        int defaultKarma,  bool init)
    {
        GameData data = new GameData
        {
            playerPosition = initialPosition,
            playerMaxHP = defaultMaxHP,
            playerHP = defaultHP,
            playerGP = defaultGP,
            playerKarma = defaultKarma,
            tilePositions = new List<Vector3>(),
            tileTypes = new List<int>(),
            init = init
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
}
