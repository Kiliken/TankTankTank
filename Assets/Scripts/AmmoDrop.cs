using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.log("Ammo refilled");
            //Play Reload SE
            Destroy(this.GameObject);
        }
    }
}
