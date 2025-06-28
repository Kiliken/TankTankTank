using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;  
    public Vector3 offset = new Vector3(0, 5, -10); 
    public float followSpeed = 1f;     
    public float rotateSpeed = 5f;      


    public float shakeDuration = 1f;
    public float shakeAmount = 70f;
    public float decreaseFactor = 1.0f;
    private Vector3 initialPosition;
    private bool isShaking = false;
    private float currentShakeDuration;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);


        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking)
        {
            if (currentShakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeAmount;
                currentShakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                isShaking = false;
                transform.localPosition = initialPosition;
            }
        }

        //if(Input.GetButtonDown("AButton"))
        //{
        //    Shake();
        //}
    }

    public void Shake()
    {
        initialPosition = transform.localPosition;
        currentShakeDuration = shakeDuration;
        isShaking = true;
        Debug.Log("Shaking");
    }
}
