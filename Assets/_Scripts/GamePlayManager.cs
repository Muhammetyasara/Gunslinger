using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

/* UPDATE
 * Box objesinde silahlar vurduðumuz hedef sayýsýna göre çýkacak
 */

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager instance;

    public SaveManager saveManager;
    public Player player;
    public PlayerMovement playerMovement;
    public ChestParent chestParent;
    public Selectablegun selectablegun;
    public Gun[] guns;
    public Box box;
    public RocketLauncher rocketLauncher;

    public CinemachineVirtualCamera[] CMcams;

    public Transform bulletParent;
    public Transform targetHolder;
    public Collider upgradeArea;

    public ParticleSystem wallExplosionVFX;
    public ParticleSystem bloodExplosionEffect;
    public ParticleSystem flashExplosionVFX;
    public ParticleSystem[] flameVFXs;

    public bool isGunActive = true;
    public bool isPlayerFinished;

    float beltX = -3.88f;
    float beltY = .7f;

    public MainTargets[] mainTargets;
    public List<Transform> redTargets = new List<Transform>();
    public List<Transform> greenTargets = new List<Transform>();
    public List<Transform> yellowTargets = new List<Transform>();

    public TextMeshProUGUI totalHitTMP;
    public TextMeshProUGUI totalHitTextTMP;
    public int totalHitValue;

    public bool isMoneyRush;

    private void Awake()
    {
        instance = this;

        if (SceneManager.GetActiveScene().name == "MoneyRush")
        {
            isMoneyRush = true;
        }
        if (isMoneyRush)
        {
            player.enabled = false;
            rocketLauncher.gameObject.SetActive(true);
        }
        else
        {
            //player.gun = guns[saveManager.GetGunIndex()];
            //Debug.Log(saveManager.GetGunIndex());
        }
    }
    public void JumpToBelt(Transform objTransform, Vector3 objPos)
    {
        Vector3 newPos = new Vector3(beltX, beltY, objPos.z);
        objTransform.DOJump(newPos, 2f, 1, .5f);
    }

    public void JumpToMainObj(Transform objTransform)
    {
        objTransform.DOJump(targetHolder.position, 1, 1, 1f);

        switch (objTransform.GetComponent<Collectible>().targetColor.targetColor)
        {
            case Enums.TargetColor.Red:
                if (redTargets.Contains(objTransform))
                {
                    return;
                }
                redTargets.Add(objTransform);
                break;
            case Enums.TargetColor.Green:
                if (greenTargets.Contains(objTransform))
                {
                    return;
                }
                greenTargets.Add(objTransform);
                break;
            case Enums.TargetColor.Yellow:
                if (yellowTargets.Contains(objTransform))
                {
                    return;
                }
                yellowTargets.Add(objTransform);
                break;
            default:
                break;
        }
    }

    public void JumpToMainTargets()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        foreach (var item in redTargets)
        {
            item.DOJump(mainTargets[0].transform.position, 1, 1, 1f);
            item.DORotate(new Vector3(0, 90, 0), 1f);
            yield return new WaitForSeconds(.09f);
        }
        foreach (var item in greenTargets)
        {
            item.DOJump(mainTargets[1].transform.position, 3, 1, 1f);
            item.DORotate(new Vector3(0, 90, 0), 1f);
            yield return new WaitForSeconds(.09f);
        }
        foreach (var item in yellowTargets)
        {
            item.DOJump(mainTargets[2].transform.position, 4, 1, 1f);
            item.DORotate(new Vector3(0, 90, 0), 1f);
            yield return new WaitForSeconds(.09f);
        }
        StartCoroutine(DamagePlus());
    }
    IEnumerator DamagePlus()
    {
        yield return new WaitForSeconds(.5f);
        mainTargets[0].valueTMP.transform.DOMove(totalHitTMP.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        mainTargets[0].gameObject.SetActive(false);
        totalHitValue += mainTargets[0].value;
        totalHitTMP.text = totalHitValue.ToString();
        mainTargets[1].valueTMP.transform.DOMove(totalHitTMP.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        mainTargets[1].gameObject.SetActive(false);
        totalHitValue += mainTargets[1].value;
        totalHitTMP.text = totalHitValue.ToString();
        mainTargets[2].valueTMP.transform.DOMove(totalHitTMP.transform.position, 1f);
        yield return new WaitForSeconds(1f);
        mainTargets[2].gameObject.SetActive(false);
        totalHitValue += mainTargets[2].value;
        totalHitTMP.text = totalHitValue.ToString();
        yield return new WaitForSeconds(1.7f);
        selectablegun.isUpgradeBegan = true;
        totalHitTextTMP.gameObject.SetActive(false);
        totalHitTMP.gameObject.SetActive(false);
        upgradeArea.enabled = false;
        StartCoroutine(selectablegun.SelectGun(totalHitValue));
        yield return new WaitForSeconds(7.2f);
        ChangeGun(guns[selectablegun.gunIndex]);
        box.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        player.gun.isExplosive = true;
        yield return new WaitForSeconds(2f);
        playerMovement.isUpgrade = false;
        StartCoroutine(FlameEffectsOpening());
    }

    public void ChangeGun(Gun gun)
    {
        foreach (var item in guns)
        {
            item.gameObject.SetActive(false);
        }
        flashExplosionVFX.Play();
        gun.gameObject.SetActive(true);
        player.gun = gun;
        playerMovement.currentGun = gun.gameObject;
    }

    public void LevelFail()
    {
        player.enabled = false;
        playerMovement.enabled = false;
    }
    public void ChestOpening()
    {
        isGunActive = false;
        if (bulletParent != null)
        {
            Destroy(bulletParent.gameObject);
        }
        player.enabled = false;
        playerMovement.enabled = false;
        //GameManager.instance.LevelComplete(1);
    }

    public void TargetDeathEffect(Vector3 pos)
    {
        ParticleSystem particle = Instantiate(bloodExplosionEffect, pos, bloodExplosionEffect.transform.rotation);
        Destroy(particle, 2f);
    }

    public IEnumerator FlameEffectsOpening()
    {
        for (int i = 0; i < flameVFXs.Length; i++)
        {
            flameVFXs[i].gameObject.SetActive(true);
            flameVFXs[i + 1].gameObject.SetActive(true);
            yield return new WaitForSeconds(.3f);
            i++;
        }
    }

    public IEnumerator GoldRainWall(Vector3 pos)
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.05f);
            GameManager.instance.CollectGem(pos, 2);
        }
    }
}