using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Textcontroll : MonoBehaviour
{
    public Text ChatText;
    public Text CharacterName;

    public GameObject Button1;
    public GameObject Button2;

    public string writerText = "";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextPractice());
        Button1.SetActive(false);
        Button2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator NomalChat(string narrator, string narration)
    {
        int a = 0;
        CharacterName.text = narrator;
        writerText = "";
        for (a = 0; a < narration.Length; a++)
        {
            writerText += narration[a];
            ChatText.text = writerText;
            yield return null;
        }
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
    }
    IEnumerator TextPractice()
    {
        if (PlayerController.pKarma >= 0)
        {
            yield return StartCoroutine(NomalChat("전사", "카르마 선 엔딩 대사 1"));
            yield return StartCoroutine(NomalChat("전사", "카르마 선 엔딩 대사 2"));
            yield return StartCoroutine(NomalChat("전사", "카르마 선 엔딩 대사 3"));
        }
        else
        {
            yield return StartCoroutine(NomalChat("전사", "카르마 악 엔딩 대사 1"));
            yield return StartCoroutine(NomalChat("전사", "카르마 악 엔딩 대사 2"));
            yield return StartCoroutine(NomalChat("전사", "카르마 악 엔딩 대사 3"));
        }

        ShowButton();
    }

    void ShowButton()
    {
        Button1.SetActive(true);
        Button2.SetActive(true);
    }
}


