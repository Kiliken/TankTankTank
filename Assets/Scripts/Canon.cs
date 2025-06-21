using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] Transform orientation;
    private Vector3 originalRelativePosition;

    // Start is called before the first frame update
    void Start()
    {
        originalRelativePosition = transform.position - transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        transform.position = orientation.position + transform.parent.position * 0.1f;
        
    }
    public void ReturnPos()
    {
        transform.position = orientation.position + transform.parent.position * 0.12f;
    }
}
