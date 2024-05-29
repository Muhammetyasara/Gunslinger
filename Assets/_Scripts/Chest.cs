using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour
{
    ChestParent chestParent;
    AnimationController animationController;

    public Gun[] guns;
    Gun currentGun;
    public Transform partsParent;
    public float partEndPos;
    public float gunPartMoveDuration;
    public ParticleSystem bulletDecal;
    public ParticleSystem smokeVFX;

    public TextMeshProUGUI healthTMP;

    public int health;

    public float rotateYValue;
    bool isGunPartRotating;
    Vector3 rotatePos;

    public bool isLevelCompleted;
    public bool isFinish;

    public int revealGunIndex;

    private void Awake()
    {
        animationController = GetComponent<AnimationController>();
        chestParent = transform.parent.GetComponent<ChestParent>();
    }

    private void Update()
    {
        if (isFinish)
        {
            if (!(health <= 0))
            {
                healthTMP.text = health.ToString();
            }
            else
            {
                healthTMP.text = null;
                GamePlayManager.instance.CMcams[1].enabled = true;
            }
            if (isGunPartRotating)
            {
                RotateGunPart();
            }
            if (isLevelCompleted)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    smokeVFX.Play();
                    guns[revealGunIndex].gameObject.SetActive(false);
                    GameManager.instance.LevelComplete();
                    isLevelCompleted = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && GamePlayManager.instance.isPlayerFinished)
        {
            DecreaseHealth(other.GetComponent<Bullet>().bulletDamage);
            transform.DOPunchPosition(new Vector3(0, 0, .5f), .05f);
            Destroy(other.gameObject);
            if (health <= 0)
            {
                GamePlayManager.instance.ChestOpening();
                chestParent.isOpening = true;
                GetRandomGunPart(gunPartMoveDuration);
                animationController.isOpen = true;
            }
            else
            {

            }
        }
    }
    public void DecreaseHealth(int damage)
    {
        health -= damage;
        bulletDecal.Play();
    }

    private void GetRandomGunPart(float duration)
    {
        int r = Random.Range(3, 14);
        if (r == SaveManager.instance.GetGunIndex())
        {
            r--;
            SaveManager.instance.SetGunIndex(r);
        }
        else
        {
            SaveManager.instance.SetGunIndex(r);
        }

        Debug.Log("random atýlan sayý" + r);
        Debug.Log("setlenen gun ýndex" + SaveManager.instance.GetGunIndex());
        guns[r].gameObject.SetActive(true);
        guns[r].transform.DOLocalMoveY(partEndPos, duration);
        currentGun = guns[r];
        revealGunIndex = r;
        isGunPartRotating = true;
        UIManager.instance.takePartText.SetActive(true);
        isLevelCompleted = true;
    }

    private void RotateGunPart()
    {
        rotatePos = partsParent.eulerAngles;
        rotatePos.y += rotateYValue;
        partsParent.eulerAngles = rotatePos;
    }
}