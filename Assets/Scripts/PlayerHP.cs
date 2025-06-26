using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private bool isPlayer = true;
    [SerializeField] private HPBar hpBar;
    [SerializeField] private TextMeshProUGUI endGameText;
    public int maxHP = 5;
    public int currentHP = 5;
    public bool isDead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        endGameText.text = string.Empty;  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveDamage(int dmg)
    {
        if (isDead) return;
        
        currentHP = Mathf.Max(0, currentHP - dmg);

        hpBar.UpdateHP((float)currentHP/(float)maxHP);

        if (currentHP <= 0)
        {
            if (isPlayer)
            {
                // SHOW YOU LOSE TEXT
                endGameText.text = "YOU LOSE";
                Debug.Log("you lose");
            }
            else
            {
                // SHOW YOU WIN TEXT
                endGameText.text = "YOU WIN";
                Debug.Log("you win");
            }

            isDead = true;
        }
    }
}
