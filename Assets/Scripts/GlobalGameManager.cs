using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    public static GlobalGameManager Instance { get; private set; }

    public List<Vector3> disabledBattleTilePositions = new List<Vector3>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisableBattleTile(Vector3 position)
    {
        if (!disabledBattleTilePositions.Contains(position))
        {
            disabledBattleTilePositions.Add(position);
        }
    }
}
