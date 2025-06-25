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
        force = (Mathf.Abs(rightTrigger) + Mathf.Abs(leftTrigger));
        float accForce = Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + (Mathf.Pow(rb.velocity.z, 2)));

        if (accForce < 20f)
        {
            //rb.AddForce

            rb.AddForce(transform.forward * force * Time.deltaTime * 100f);
        }
        if (velocity > 0)
        {

        }

        rotateVelocity = diff;
        if (rotateVelocity > 0)
        {
            rotateVelocity = Mathf.Min(rotateVelocity, 0.5f);

        }
        else if (rotateVelocity < 0)
        {
            rotateVelocity = Mathf.Max(rotateVelocity, -0.5f);
        }

        rb.AddTorque(0, rotateVelocity * Time.deltaTime * 10f, 0);
        

        
        Debug.Log(rb.velocity);
        Debug.Log(accForce);

        if (diff != 0)
        {
            
            rb.velocity = Vector3.zero;
            rb.velocity = new Vector3(accForce * Mathf.Sin(diff * Time.deltaTime * 10f), 0, accForce * Mathf.Cos(diff * Time.deltaTime * 10f));

        }


      

        
    }
}
