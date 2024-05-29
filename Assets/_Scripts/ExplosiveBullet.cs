using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    Rigidbody rb;

    public ParticleSystem shineVFX;

    public float bulletSpeed;
    public int bulletDamage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ShotFired();
    }

    public void ShotFired()
    {
        rb.velocity = bulletSpeed * Time.deltaTime * Vector3.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 17)
        {
            GamePlayManager.instance.wallExplosionVFX.Play();
            shineVFX.Stop();
            Destroy(other.gameObject, 1);
            Destroy(gameObject, .5f);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}