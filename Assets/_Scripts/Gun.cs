using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public PlayerMovement playerMovement;
    public Player player;

    public Transform[] muzzle;
    public ParticleSystem muzzleFlash;
    float timeSinceLastShot;
    public float fireRate;
    public float maxDistance;
    public bool isUpgrade;
    public bool isEnteringUpgrade;

    public ExplosiveBullet explosiveBullet;
    public bool isExplosive;

    string gateBuffType;
    float gateValue;

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        player = FindAnyObjectByType<Player>();
    }

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
            Shoot(gunData.muzzleCount);
        }
    }

    private bool CanShoot() => timeSinceLastShot > 1f / (fireRate / 60f);

    private void Shoot(int bulletCount)
    {
        if (CanShoot() && !playerMovement.isUpgrade && !isEnteringUpgrade)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet createdBullet = Instantiate(gunData.bullet, muzzle[i].position, gunData.bullet.transform.rotation, GamePlayManager.instance.bulletParent);
                createdBullet.bulletDamage = gunData.damage;
                createdBullet.maxDistance = maxDistance;
            }
            muzzleFlash.Play();
            timeSinceLastShot = 0f;
        }
        if (isExplosive)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                ExplosiveBullet createdBullet = Instantiate(explosiveBullet, muzzle[i].position, explosiveBullet.transform.rotation, GamePlayManager.instance.bulletParent);
                createdBullet.bulletDamage = gunData.damage;
            }
            muzzleFlash.Play();
            timeSinceLastShot = 0f;
            isExplosive = false;
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
            GamePlayManager.instance.chestParent.GetActiveChest().isFinish = true;
        }
        if (other.gameObject.layer == 15)
        {
            isEnteringUpgrade = true;
        }
        if (other.gameObject.layer == 14)
        {
            if (!isUpgrade)
            {
                GamePlayManager.instance.JumpToMainTargets();
                isUpgrade = true;
            }
        }
        if (other.gameObject.CompareTag("UpgradeArea"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            playerMovement.forwardSpeed = 5f;
        }
        if (other.gameObject.layer == 14)
        {
            playerMovement.isUpgrade = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            playerMovement.forwardSpeed = 10f;
        }
        if (other.gameObject.layer == 14)
        {
            isUpgrade = false;
            isEnteringUpgrade = false;
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
}