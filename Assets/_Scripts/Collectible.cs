using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Collectible : MonoBehaviour
{
    Rigidbody rb;
    Collider col;

    public Enums targetColor;

    public ParticleSystem[] bloodEffects;
    public TextMeshProUGUI healthTMP;

    public int health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!(health <= 0))
        {
            healthTMP.text = health.ToString();
        }
        else
        {
            healthTMP.text = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            DecreaseHealth(other.GetComponent<Bullet>().bulletDamage);
            transform.DOPunchPosition(new Vector3(0, 0, .5f), .05f);
            Destroy(other.gameObject);
            if (health <= 0)
            {
                GamePlayManager.instance.TargetDeathEffect(transform.position);
                GamePlayManager.instance.JumpToBelt(transform, transform.position);
            }
            else
            {
                int r = Random.Range(0, 2);
                bloodEffects[r].Play();
            }
        }
        if (other.gameObject.layer == 6)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            col.isTrigger = false;
        }
        if (other.gameObject.layer == 10)
        {
            col.isTrigger = false;
            transform.DOMove(other.transform.position, .1f);
        }
        if (other.gameObject.layer == 12)
        {
            GamePlayManager.instance.JumpToMainObj(transform);
            transform.DORotate(new Vector3(0, 90, 90), .4f);
            rb.isKinematic = true;
        }
        if (other.gameObject.layer == 13)
        {
            Destroy(this.gameObject, .1f);
        }
        if (other.gameObject.layer == 20)
        {
            GameManager.instance.CollectGem(transform.position, 1);
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth(int damage)
    {
        health -= damage;
    }
}