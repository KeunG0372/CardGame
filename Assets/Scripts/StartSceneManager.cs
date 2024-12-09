using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public DataManager dataManager;
    public Vector3 initialPosition = new Vector3(-6, 0, -1);
    public int playerMaxHP = 20;
    public int playerHP = 20;
    public int playerGP = 2;
    public int playerKarma = 0;

    public void StartGameing()
    {
        dataManager.ResetGameData(initialPosition, playerMaxHP, playerHP, playerGP, playerKarma, true);
        PlayerController.plGold = 100;

        GameData data = dataManager.LoadGameData();
        if (data != null)
        {
            Debug.Log($"Game data saved: Position={data.playerPosition}, HP={data.playerHP}, GP={data.playerGP}");
        }
        else
        {
            Debug.LogError("Game data reset failed.");
        }

        SceneManager.LoadScene("MovingScene");
    }

    public void LoadGameing()
    {
        GameData data = dataManager.LoadGameData();
        SceneManager.LoadScene("MovingScene");
    }

    public void QuitGameing()
    {
        Application.Quit();
    }

    public void EndGameing()
    {
        SceneManager.LoadScene("IntroScene");
    }

}
