using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image hpBar;
    
    private float fullWidth;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetComponent<Image>();
        //UpdateHP(0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHP(float hpPercent)
    {
        hpBar.fillAmount = hpPercent;
    }
}
