using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameing()
    {
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
