using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffectPrefab;
    [SerializeField] private AudioClip cannonSoundEffect;
    [SerializeField] private AudioClip cannonReloadSoundEffect;
    [SerializeField] private AudioClip hitSoundEffect;
    [SerializeField] private AudioClip rumbleSoundEffect;
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
    }

    public void Fire()
    {
        transform.position -= transform.forward * 0.25f;
        muzzleFlash.Play();
        audioSource.PlayOneShot(cannonSoundEffect, 0.8f);
        audioSource.PlayOneShot(cannonReloadSoundEffect);


        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f, LayerMask.GetMask("Player", "Environment")))
        {
            if (hit.collider.gameObject.tag == "Hurtbox")
            {
                Debug.Log("Shot player");
                hit.collider.gameObject.GetComponent<PlayerHP>().ReceiveDamage(damage);
                audioSource.PlayOneShot(hitSoundEffect, 0.8f);
            }
            else
            {
                audioSource.PlayOneShot(hitSoundEffect, 0.2f);
                audioSource.PlayOneShot(rumbleSoundEffect, 1.5f);
            }
            PlayEffectAtPoint(hit.point, hit.normal); //here
            
        }

        
        
        camera.Shake();
    }


    void PlayEffectAtPoint(Vector3 position, Vector3 normal)
    {
        GameObject effect = Instantiate(impactEffectPrefab, position, Quaternion.LookRotation(normal));
        Destroy(effect, 2f);
    }

    public void ReturnPos()
    {
        transform.position += transform.forward * 0.25f;
    }
}
