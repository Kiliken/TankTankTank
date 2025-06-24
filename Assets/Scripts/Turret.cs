using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private float turnSpeed = 30f;
    [SerializeField] private Cannon cannon;

    public float fadeSpeed = 2f;
    public float activeVolume = 1f;
    public float idleVolume = 0f;
    public float pitch = 1.5f; 
    
    [SerializeField] private AudioSource turretAudio;

    private bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        if (turretAudio != null)
        {
            turretAudio.pitch = pitch;
            turretAudio.loop = true;
            turretAudio.volume = 0f;
            turretAudio.Play(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (turretAudio != null)
        {
            float targetVolume = isTurning ? activeVolume : idleVolume;
            turretAudio.volume = Mathf.MoveTowards(turretAudio.volume, targetVolume, fadeSpeed * Time.deltaTime);
            isTurning = false;
        }
    }

    public void TurnLeft()
    {
        //Debug.Log("turnLeft");
        transform.Rotate(0,0,-turnSpeed * Time.deltaTime);
        isTurning = true;
    }

    public void TurnRight()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        isTurning = true;
    }

    public void Fire()
    {
        cannon.Fire();
    }

    public void ReturnPos()
    {
        cannon.ReturnPos();
    }
}
