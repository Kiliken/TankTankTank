using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

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

        if(Input.GetButtonDown("AButton"))
        {
            Shake();
        }
    }

    public void Shake()
    {
        initialPosition = transform.localPosition;
        currentShakeDuration = shakeDuration;
        isShaking = true;
        Debug.Log("Shaking");
    }
}
