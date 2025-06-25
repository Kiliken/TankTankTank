using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip cannonSoundEffect;
    [SerializeField] private AudioClip cannonReloadSoundEffect;
    private AudioSource audioSource;
    [SerializeField] private CameraControl camera;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f, LayerMask.GetMask("Ammo")))
        {
            Debug.Log("Hit");
        }
    }

    public void Fire()
    {
        transform.position -= transform.forward * 0.5f;
        muzzleFlash.Play();
        audioSource.PlayOneShot(cannonSoundEffect);
        audioSource.PlayOneShot(cannonReloadSoundEffect);
        camera.Shake();
    }
    public void ReturnPos()
    {
        transform.position += transform.forward * 0.5f;
    }
}
