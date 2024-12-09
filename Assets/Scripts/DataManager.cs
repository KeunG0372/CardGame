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
    public int playerMaxHP;
    public int playerHP;  // �÷��̾� ü��
    public int playerGP;  // �÷��̾� ���� ����Ʈ
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
        GameData data = LoadGameData() ?? new GameData();  // ���� �����͸� �ҷ����ų� ���� ����

        // null ���� �ƴ� ��쿡�� �����͸� ������Ʈ
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

#pragma warning disable CS0472 // �� ������ ���� 'null'�� ���� �� �����Ƿ� ���� ����� �׻� �����մϴ�.
            if (data.init == null)
            {
                data.init = false;
            }
#pragma warning restore CS0472 // �� ������ ���� 'null'�� ���� �� �����Ƿ� ���� ����� �׻� �����մϴ�.

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
