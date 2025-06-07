  using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float leftTrigger;
    [SerializeField] private float rightTrigger;

    [SerializeField] private float velocity = 0;
    private float maxVelocity;
    [SerializeField] private float force = 0;
    [SerializeField] private float diff;
    [SerializeField] private float rotateVelocity = 0;
    [SerializeField] private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        diff = leftTrigger - rightTrigger;
        rotateVelocity += diff;
        if(rotateVelocity > 0)
        {
            rotateVelocity = Mathf.Min(rotateVelocity, 0.5f);

        }
        else if (rotateVelocity < 0)
        {
            rotateVelocity = Mathf.Max(rotateVelocity, -0.5f);
        }

        transform.Rotate(0, rotateVelocity * Time.deltaTime * 100f, 0);


        force = (Mathf.Abs(rightTrigger) + Mathf.Abs(leftTrigger) - Mathf.Abs(diff));
        velocity += force * Time.deltaTime;
        if (velocity < 10f)
        {
            rb.AddForce(transform.forward * force * Time.deltaTime * 100f);
        }
        
    }
}
