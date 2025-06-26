using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private bool isPlayer = true;
    public int maxHP = 5;
    public int currentHP = 5;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReceiveDamage(int dmg)
    {
        if (isDead) return;
        
        currentHP = Mathf.Max(0, currentHP - dmg);

        // update HP UI

        if (currentHP <= 0)
        {
            if (isPlayer)
            {
                // SHOW YOU LOSE TEXT
                Debug.Log("you lose");
            }
            else
            {
                // SHOW YOU WIN TEXT
                Debug.Log("you win");
            }

            isDead = true;
        }
    }
}
