using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TankMovement2 : MonoBehaviour
{
    Rigidbody rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] float moveSpeedDefault = 10f;
    [SerializeField] float moveSpeedDash = 100f;

    [SerializeField] private AudioSource engineAudioSource;
    [SerializeField] private float moveThreshold = 0.1f;
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float fadeSpeed = 2f; // How fast volume changes
    [SerializeField] private float minVolume = 0.1f;

    [SerializeField] private bool enableKeyboard;

    private float horizontalInput;
    private float verticalInput;
    private float leftTrigger;  // 0 to 1
    private float rightTrigger; // 0 to 1

    private float moveInput;
    private float rotInput;

    [SerializeField] Turret turret;

    private Vector3 moveDirection;
    Transform orientation;
    private Quaternion targetModelRotation;
    private Vector3 rotDirection;

    //private float returnInterval = 0.5f;
    //[SerializeField] private float sinceFire = 0f;
    //private bool returned = true;

    public byte shooting = 0x00;
    private bool shot = false;
    private float shootCD = 2.5f;
    private float shootCDTimer = 0f;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        orientation = gameObject.transform;

        engineAudioSource.volume = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

        SoundControl();

        //if (shooting)
        if (shot)
        {
            if (shootCDTimer < shootCD)
            {
                shootCDTimer += Time.deltaTime;
            }
            else
            {
                shot = false;
                //shooting = false;
                shootCDTimer = 0f;
                Debug.Log("can shoot");
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
        SpeedControl();
        if(rotInput != 0)
        {
            targetModelRotation = Quaternion.LookRotation(rotDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetModelRotation, Time.deltaTime);

        }

    }

    private void LateUpdate()
    {
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        leftTrigger = Input.GetAxisRaw("leftTrigger");
        rightTrigger = Input.GetAxisRaw("rightTrigger");
        if (leftTrigger <= 0 && Input.GetButton("LB"))
        {
            leftTrigger = -1;
        }
        if (rightTrigger <= 0 && Input.GetButton("RB"))
        {
            rightTrigger = -1;
        }

        if(enableKeyboard)
        {
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
            // if (Input.GetKeyDown(KeyCode.Space))
            // {
            //     turret.Fire();
            //     sinceFire = 0f;
            //     returned = false;
            // }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!shot)
                {
                    turret.Fire();
                    shooting += 0x01;
                    if (shooting >= 0x10)
                        shooting -= 0x10;
                    //sinceFire = 0f;
                    //returned = false;
                    shot = true;
                    Debug.Log("shot");
                }
            }
            if (Input.GetKey(KeyCode.J))
            {
                turret.TurnLeft();
            }
            if (Input.GetKey(KeyCode.L))
            {
                turret.TurnRight();
            }
        }



        //if (Input.GetButton("LB"))
        //{
        //    //Debug.Log("lbPRESSED");
        //    leftTrigger = -leftTrigger;
        //}
        //if (Input.GetButton("RB"))
        //{
        //    rightTrigger = -rightTrigger;
        //}

        if (rightTrigger + leftTrigger == 0)
        {
            moveInput = 0;
        }
        else if(rightTrigger + leftTrigger > 0)
        {
            moveInput = (rightTrigger + leftTrigger - Mathf.Abs(leftTrigger - rightTrigger) / 2) / 2;
        }
        else if (rightTrigger + leftTrigger < 0)
        {
            moveInput = (rightTrigger + leftTrigger + Mathf.Abs(leftTrigger - rightTrigger) / 2) / 2;
        }

        rotInput = (leftTrigger - rightTrigger) / 2;
        

        moveSpeed = (moveInput != 0) ? moveSpeedDefault : 0f;

        if (horizontalInput < 0)
        {
            turret.TurnLeft();
        }

        if (horizontalInput > 0) {
            turret.TurnRight();
        }


        //if (Input.GetButtonDown("AButton"))
        //{
        //    turret.Fire();
        //    sinceFire = 0f;
        //    returned = false;
        //}

        if (Input.GetButtonDown("AButton"))
        {
            
            if (!shot)
            {
                turret.Fire();
                shooting += 0x01;
                //sinceFire = 0f;
                //returned = false;
                shot = true;
                Debug.Log("shot");
            }
        }


        //sinceFire += Time.deltaTime;
        //if (sinceFire > 0.1f && returned == false)
        //{
        //    returned = true;
        //    turret.ReturnPos();
        //    sinceFire = 0;
        //}
    }

    private void Movement()
    {
        moveDirection = orientation.forward * moveInput + orientation.right * 0;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        rotDirection = orientation.forward * 0 + orientation.right * rotInput;
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

    private void SoundControl()
    {
        float speed = rb.velocity.magnitude;
        float targetVolume = (speed > moveThreshold) ? maxVolume : minVolume;

        engineAudioSource.volume = Mathf.MoveTowards(engineAudioSource.volume, targetVolume, fadeSpeed * Time.deltaTime);
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
            
    }
}
