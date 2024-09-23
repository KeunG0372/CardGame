using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Inst { get; private set; }

    void Awake() => Inst = this;

    [SerializeField] MsSO msSO;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform[] spawnPoints;

    List<Monster> activeMonsters = new List<Monster>();

    private void Start()
    {
        SpawnMonster(msSO.monsters[0], spawnPoints[0]);
    }

    public void SpawnMonster(MonsterInfo monsterInfo, Transform spawnPoint)
    {
        var monsterObject = Instantiate(monsterPrefab, spawnPoint.position, Utils.QI);
        var monster = monsterObject.GetComponent<Monster>();
        monster.Setup(monsterInfo);
        activeMonsters.Add(monster);
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);
        Destroy(monster.gameObject);
    }

    public List<Monster> GetActiveMonsters()
    {
        return new List<Monster>(activeMonsters);
    }
}
