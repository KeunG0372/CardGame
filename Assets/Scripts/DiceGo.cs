using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceGo : MonoBehaviour
{
    private int numbers;
    public int goNumb;
    public Text diceText;
    public GameObject dicesWitch;

    public GameObject playerMoves;

    public Vector3 initialPosition = new Vector3(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerPosition();

        //playerMoves.transform.position = initialPosition;

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(rollingdice());

    }

    IEnumerator rollingdice()
    {
        yield return new WaitForSeconds(1);

        numbers = Random.RandomRange(1, 7);
        if (DiceRoll.isroll == true)
        {
            goNumb = numbers;
            diceText.text = numbers.ToString();
            //PlayerMove.numbers = numbers;
            DiceRoll.isroll = false;
            dicesWitch.SetActive(false);
            StartCoroutine(nowEnd());

        }

    }

    IEnumerator nowEnd()
    {
        yield return new WaitForSeconds(1);

        dicesWitch.SetActive(true);
        gameObject.SetActive(false);
        diceText.text = "end";
        PlayerMoving();

    }

    void PlayerMoving()
    {
        Vector3 move = Vector3.zero;

        if (goNumb == 1) move = new Vector3(1.2f, 0, 0);
        else if (goNumb == 2) move = new Vector3(2.4f, 0, 0);
        else if (goNumb == 3) move = new Vector3(3.6f, 0, 0);
        else if (goNumb == 4) move = new Vector3(4.8f, 0, 0);
        else if (goNumb == 5) move = new Vector3(6f, 0, 0);
        else if (goNumb == 6) move = new Vector3(7.2f, 0, 0);

        playerMoves.transform.position += move;
        goNumb = 0;

        SavePlayerPosition();
    }

    void ResetPlayerPosition()
    {
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.Save();
    }

    void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerX", playerMoves.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerMoves.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", playerMoves.transform.position.z);
        PlayerPrefs.Save();
    }

}
