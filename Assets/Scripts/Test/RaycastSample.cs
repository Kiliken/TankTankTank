using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f))
        {
            Debug.Log("Hit");
        }
    }
}
