using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterInfo
{
    public string name;
    public int msHealth;
    public int msGuard;
    public int msDamage;
    public Sprite sprite;
    public int index;
}


[CreateAssetMenu(fileName = "MsSO", menuName = "Scriptable Object/MsSO")]
public class MsSO : ScriptableObject
{
    public MonsterInfo[] monsters;
}
