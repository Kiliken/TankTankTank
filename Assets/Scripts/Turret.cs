using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float turnSpeed = 30f;
    [SerializeField] private Canon canon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnLeft()
    {
        //Debug.Log("turnLeft");
        transform.Rotate(0,0,-turnSpeed * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }

    public void Fire()
    {
        canon.Fire();
    }

    public void ReturnPos()
    {
        canon.ReturnPos();
    }
}
