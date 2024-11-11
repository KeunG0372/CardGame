using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public DataManager dataManager;
    public Vector3 initialPosition = new Vector3(-6, 0, -1);

    public void StartGameing()
    {
        dataManager.ResetGameData(initialPosition);

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
