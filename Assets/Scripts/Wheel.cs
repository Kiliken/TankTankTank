using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (isRight)
        {
            if (horizontalInput > 0)
            {
                RotateForward();
            }
            else if (horizontalInput < 0) 
            {
                RotateBackward();
            }
        }
        else if (!isRight)
        {
            if (verticalInput > 0)
            {
                RotateForward();
            }
            else if (verticalInput < 0)
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
