using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    private float leftTrigger;
    private float rightTrigger;

    [SerializeField] private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        leftTrigger = Input.GetAxisRaw("leftTrigger");
        rightTrigger = Input.GetAxisRaw("rightTrigger");
        if (Input.GetKey(KeyCode.Alpha1))
        {
            leftTrigger = 1;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            leftTrigger = -1;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            rightTrigger = 1;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rightTrigger = -1;
        }

        if (isRight)
        {
            if (rightTrigger > 0)
            {
                RotateForward();
            }
            else if (rightTrigger < 0) 
            {
                RotateBackward();
            }
        }
        else if (!isRight)
        {
            if (leftTrigger > 0)
            {
                RotateForward();
            }
            else if (leftTrigger < 0)
            {
                RotateBackward();
            }
        }
    }

    private void RotateForward()
    {
        transform.Rotate(0, 0, Time.deltaTime * 300f);
    }

    private void RotateBackward()
    {
        transform.Rotate(0, 0, Time.deltaTime * -300f);
    }
}
