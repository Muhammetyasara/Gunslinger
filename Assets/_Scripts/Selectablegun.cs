using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Selectablegun : MonoBehaviour
{
    public ChestParent chestParent;

    public GameObject[] guns;

    Vector3 newRot;
    public bool isUpgradeDone;
    public bool isUpgradeBegan;

    public int gunIndex;

    public Transform glowVFX;
    public IEnumerator SelectGun(int value)
    {
        foreach (GameObject item in guns)
        {
            item.SetActive(true);
            yield return new WaitForSeconds(.4f);
            item.SetActive(false);
        }
        if (value == 0)
        {
            gunIndex = value;
            chestParent.ChestReveal(0);
        }
        if (value > 0 && value < 10)
        {
            int r = Random.Range(3, 6);
            if (r == SaveManager.instance.GetGunIndex())
            {
                r--;
            }
            gunIndex = r;
            chestParent.ChestReveal(1);
        }
        if (value >= 10 && value < 20)
        {
            int r = Random.Range(6, 9);
            if (r == SaveManager.instance.GetGunIndex())
            {
                r--;
            }
            gunIndex = r;
            chestParent.ChestReveal(2);
        }
        if (value >= 20 && value < 30)
        {
            int r = Random.Range(9, 11);
            if (r == SaveManager.instance.GetGunIndex())
            {
                r--;
            }
            gunIndex = r;
            chestParent.ChestReveal(3);
        }
        if (value >= 30)
        {
            int r = Random.Range(11, 14);
            if (r == SaveManager.instance.GetGunIndex())
            {
                r--;
            }
            gunIndex = r;
            chestParent.ChestReveal(4);
        }

        GameObject[] guns1 = guns;
        guns1[gunIndex].SetActive(true);
        yield return new WaitForSeconds(.2f);
        isUpgradeDone = true;
        isUpgradeBegan = false;
    }

    private void Update()
    {
        if (!isUpgradeDone && isUpgradeBegan)
        {
            newRot = transform.eulerAngles;
            newRot.y += 1.5f;
            transform.eulerAngles = newRot;
        }
        if (isUpgradeDone && !isUpgradeBegan)
        {
            transform.DOJump(Player.instance.gun.transform.position, 1, 1, 1f);
            glowVFX.DOJump(Player.instance.gun.transform.position, 1, 1, 1f);
            newRot = transform.eulerAngles;
            newRot.y = Mathf.LerpAngle(newRot.y, 0, Time.deltaTime * 4f);
            transform.eulerAngles = newRot;
        }
    }
}