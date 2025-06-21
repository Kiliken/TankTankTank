using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        transform.position -= transform.forward * 0.5f;
        muzzleFlash.Play();
        
    }
    public void ReturnPos()
    {
        transform.position += transform.forward * 0.5f;
    }
}
