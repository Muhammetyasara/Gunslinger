using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Selectablegun selectablegun;

    public GameObject shineVFX;

    Vector3 newPos;
    public bool isUpgradeBegan;
    public bool isUpgradeDone;
    void Update()
    {
        isUpgradeBegan = selectablegun.isUpgradeBegan;
        if (isUpgradeBegan)
        {
            shineVFX.SetActive(true);
            newPos = transform.position;
            newPos.y = Mathf.Lerp(newPos.y, 0.1f, Time.deltaTime * 4);
            transform.position = newPos;
        }
        if (isUpgradeDone)
        {
            gameObject.SetActive(false);
        }
    }
}