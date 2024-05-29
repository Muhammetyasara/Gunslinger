using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTargets : MonoBehaviour
{
    public Enums targetColor;

    public ParticleSystem plusVFX;

    public TextMeshProUGUI valueTMP;
    public int value;

    private void Update()
    {
        valueTMP.text = "x" + value.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            value++;
            plusVFX.Play();
            Destroy(other.gameObject, 1f);
        }
    }
}