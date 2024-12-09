using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnUIs : MonoBehaviour
{
    public GameObject offPanel;

    [SerializeField] private int healAmount = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void NowOut()
    {
        offPanel.SetActive(false);
    }

    public void RestNow()
    {
        Debug.Log("HP Restore");
        HealPlayer();
    }

    public void HealPlayer()
    {
        int currentHealth = PlayerPrefs.GetInt("PlayerHealth", 20);  // 기본값을 20으로 설정
        int maxHealth = PlayerPrefs.GetInt("PlayerMaxHealth", 20);

        // 회복된 체력 계산 (최대 체력을 넘지 않도록)
        int newHealth = Mathf.Min(currentHealth + healAmount, maxHealth);

        // 새로운 체력 값 저장
        PlayerPrefs.SetInt("PlayerHealth", newHealth);
        PlayerPrefs.Save();

        Debug.Log("Player healed to " + newHealth + " HP in the inn.");
    }
}
