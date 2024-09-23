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


    // Start is called before the first frame update
    void Start()
    {
        
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
        if (goNumb == 1)
        {
            playerMoves.transform.position += new Vector3(1.2f, 0, 0);
            goNumb = 0;
        }
        else if (goNumb == 2)
        {
            playerMoves.transform.position += new Vector3(2.4f, 0, 0);
            goNumb = 0;

        }
        else if (goNumb == 3)
        {
            playerMoves.transform.position += new Vector3(3.6f, 0, 0);
            goNumb = 0;

        }
        else if (goNumb == 4)
        {
            playerMoves.transform.position += new Vector3(4.8f, 0, 0);
            goNumb = 0;

        }
        else if (goNumb == 5)
        {
            playerMoves.transform.position += new Vector3(6f, 0, 0);
            goNumb = 0;

        }
        else if (goNumb == 6)
        {
            playerMoves.transform.position += new Vector3(7.2f, 0, 0);
            goNumb = 0;

        }
    }

    
}
