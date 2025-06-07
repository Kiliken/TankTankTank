using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] TestManager testManager;
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] float moveSpeedDefault = 10f;
    [SerializeField] float moveSpeedDash = 100f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    Transform orientation;
    private Quaternion targetModelRotation;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        orientation = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Movement();
        SpeedControl();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveSpeed = (horizontalInput != 0 || verticalInput != 0) ? moveSpeedDefault : 0f;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            testManager.SendData(transform.position);
        }
    }

    private void Movement()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        //string pos = transform.position.ToString();
        //Debug.Log(pos);
    }

    private void SpeedControl()
    {
        Vector3 flatVet = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVet.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVet.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
