using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private bool isPlayer = true;
    public int maxHP = 5;
    public int currentHP = 5;

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
        currentHP = Mathf.Max(0, currentHP - dmg);

        // update UI

        if (currentHP <= 0)
        {
            Debug.Log("dead");
        }
    }
}
