using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    public GameObject projectileExplosionVFX;

    public float bulletSpeed;
    public float maxDistance;
    public int bulletDamage;

    float firstPosZ;
    float currentPosZ;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        firstPosZ = transform.position.z;
    }

    private void Update()
    {
        if (GameManager.instance.isLevelStarted && !GamePlayManager.instance.isMoneyRush)
        {
            BulletMaxDistance();
            ShotFired();
        }
        else if (GameManager.instance.isLevelStarted && GamePlayManager.instance.isMoneyRush)
        {
            BulletMaxDistance();
            ShotFiredProjectile();
        }
    }

    public void ShotFired()
    {
        rb.velocity = bulletSpeed * Time.deltaTime * Vector3.forward;
    }

    void ShotFiredProjectile()
    {
        rb.velocity = bulletSpeed * Time.deltaTime * Vector3.forward;
        transform.parent = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            if (GamePlayManager.instance.isMoneyRush)
            {
                GameManager.instance.CollectGem(other.transform.position, 1);
                GameObject createdVFX = Instantiate(projectileExplosionVFX, transform.position, transform.rotation, GamePlayManager.instance.transform);
                Destroy(createdVFX, 1f);
                Destroy(gameObject, .5f);
            }
            GameManager.instance.CollectGem(other.transform.position, 1);
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 14)
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.layer == 17)
        {
            if (GamePlayManager.instance.isMoneyRush)
            {
                GameObject createdVFX = Instantiate(projectileExplosionVFX, transform.position, transform.rotation, GamePlayManager.instance.transform);
                Destroy(createdVFX, .5f);
                Destroy(other.gameObject, 1f);
                Destroy(gameObject);
            }
            Destroy(other.gameObject, 1);
        }
        if (other.gameObject.CompareTag("UpgradeArea"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.layer == 7)
        {
            if (GamePlayManager.instance.isMoneyRush)
            {
                GameObject createdVFX = Instantiate(projectileExplosionVFX, transform.position, transform.rotation, GamePlayManager.instance.transform);
                Destroy(createdVFX, .5f);
                Destroy(gameObject, .5f);
            }
            else
            {
                return;
            }
        }
        if (other.gameObject.layer == 13)
        {
            if (GamePlayManager.instance.isMoneyRush)
            {
                GameObject createdVFX = Instantiate(projectileExplosionVFX, transform.position, transform.rotation, GamePlayManager.instance.transform);
                Destroy(createdVFX, .5f);
                Destroy(gameObject, .5f);
            }
            else
            {
                return;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 17)
        {
            if (GamePlayManager.instance.isMoneyRush)
            {
                GameObject createdVFX = Instantiate(projectileExplosionVFX, transform.position, transform.rotation, GamePlayManager.instance.transform);
                Destroy(createdVFX, .5f);
                Destroy(gameObject, .5f);
            }
            else
            {
                return;
            }
        }
    }

    public void BulletMaxDistance()
    {
        currentPosZ = transform.position.z;
        if (currentPosZ - firstPosZ > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}