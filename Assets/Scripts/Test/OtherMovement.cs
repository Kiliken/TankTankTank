using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherMovement : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 movePos;
    [SerializeField] float moveSpeedDefault = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeedDefault);
    }

    private void FixedUpdate()
    {

    }
}
