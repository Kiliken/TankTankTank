using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Ammo refilled");
            //Play Reload SE
            Destroy(this.gameObject);
        }
    }
}
