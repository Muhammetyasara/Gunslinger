using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MoneyRushChest : MonoBehaviour
{
    ChestParent chestParent;
    AnimationController animationController;

    public ParticleSystem bulletDecal;
    public ParticleSystem goldVFX;

    public TextMeshProUGUI healthTMP;
    public int health;

    public bool isLevelCompleted;
    public bool isFinish;

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
            if (isLevelCompleted)
            {
                GameManager.instance.LevelComplete();
                isLevelCompleted = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3 && GamePlayManager.instance.isPlayerFinished)
        {
            DecreaseHealth(other.GetComponent<Bullet>().bulletDamage);
            transform.DOPunchPosition(new Vector3(0, 0, .5f), .05f);
            if (health <= 0)
            {
                GamePlayManager.instance.ChestOpening();
                chestParent.isOpening = true;
                animationController.isOpen = true;
                StartCoroutine(GoldRain());
            }
            else
            {

            }
        }
    }

    IEnumerator GoldRain()
    {
        yield return new WaitForSeconds(.5f);
        goldVFX.gameObject.SetActive(true);
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(.05f);
            GameManager.instance.CollectGem(transform.position, 20);
        }
        isLevelCompleted = true;
    }
    public void DecreaseHealth(int damage)
    {
        health -= damage;
        bulletDecal.Play();
    }
}
