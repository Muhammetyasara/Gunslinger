using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    public GameObject sideGate;

    public TextMeshProUGUI buffType;
    public TextMeshProUGUI value;

    public Material[] gateMaterials;

    public string buffTypeString;
    public float valueInt;

    public ParticleSystem healingVFX;

    private void Start()
    {
        buffTypeString = buffType.text;
        valueInt = int.Parse(value.text);
    }

    private void Update()
    {
        if (valueInt < 0)
        {
            value.text = valueInt.ToString("0");
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            value.text = "+" + valueInt.ToString("0");
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            valueInt += 1f;
            healingVFX.Play();
            transform.parent.DOPunchPosition(new Vector3(0, 0, .5f), .1f);
            Destroy(other.gameObject);
        }
    }

    public void DestroySideGate()
    {
        Destroy(sideGate);
    }
}