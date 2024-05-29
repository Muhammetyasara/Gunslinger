using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RocketLauncher : MonoBehaviour
{
    public GunData gunData;
    public PlayerMovement playerMovement;

    public GameObject projectileMesh;
    public Transform projectileTransform;
    public ParticleSystem muzzleFlash;

    public GameObject projectilePrefab;

    float timeSinceLastShot;
    public float fireRate;
    public float maxDistance;

    string gateBuffType;
    float gateValue;
    private void Start()
    {
        fireRate = gunData.fireRate;
        maxDistance = gunData.maxDistance;
    }
    private void Update()
    {
        if (GameManager.instance.isLevelStarted && GamePlayManager.instance.isGunActive)
        {
            timeSinceLastShot += Time.deltaTime;
            Shoot();
        }
    }

    private bool CanShoot() => timeSinceLastShot > 1f / (fireRate / 60f);
    private void Shoot()
    {
        if (CanShoot())
        {
            Bullet createdBullet = Instantiate(gunData.bullet, projectileTransform.position, projectileTransform.rotation, projectileTransform);
            StartCoroutine(ShotDelay());
            createdBullet.bulletDamage = gunData.damage;
            createdBullet.maxDistance = maxDistance;

            muzzleFlash.Play();
            timeSinceLastShot = 0f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Gate gate))
        {
            gateBuffType = gate.buffTypeString;
            gateValue = gate.valueInt;
            SetBuffType(gateValue, gateBuffType);
            playerMovement.isTriggerGate = true;
            transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + 360), .4f, RotateMode.FastBeyond360);
            gate.DestroySideGate();
            Destroy(gate.transform.parent.gameObject);
            StartCoroutine(Delay(.6f));
        }
        if (other.gameObject.layer == 9)
        {
            GamePlayManager.instance.LevelFail();
            GameManager.instance.LevelFail();
            this.enabled = false;
        }
        if (other.gameObject.layer == 11)
        {
            GamePlayManager.instance.isPlayerFinished = true;
            playerMovement.forwardSpeed = 0;
            GamePlayManager.instance.chestParent.moneyRushChest.isFinish = true;
        }
    }
    private void SetBuffType(float value, string buffType)
    {
        switch (buffType)
        {
            case "MAX DISTANCE":
                maxDistance += value;
                break;
            case "FIRERATE":
                fireRate += value;
                break;
            case "DAMAGE":
                break;
            default:
                break;
        }
    }
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        playerMovement.isTriggerGate = false;
    }
    IEnumerator ShotDelay()
    {
        projectileMesh.SetActive(false);
        yield return new WaitForSeconds(.2f);
        projectileMesh.SetActive(true);
    }
}