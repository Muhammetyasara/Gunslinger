using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunParts : MonoBehaviour
{
    //public int levelValue;
    //public string partType;
    //public bool changing;

    //public int currentScope;
    //public int currentGrip;
    //public int currentLaser;
    //public int currentMuzzle;

    //public Transform[] scopes;
    //public Transform[] grips;
    //public Transform[] lasers;
    //public Transform[] muzzles;

    //private void Update()
    //{
    //    if (changing)
    //    {
    //        ChangeAnyPart(levelValue, partType);
    //    }
    //}

    //public void ChangeAnyPart(int levelValue, string partType)
    //{
    //    switch (partType)
    //    {
    //        case "Scope":
    //            ChangeScope(levelValue);
    //            break;
    //        case "Grip":
    //            ChangeGrip(levelValue);
    //            break;
    //        case "Laser":
    //            ChangeLaser(levelValue);
    //            break;
    //        case "Muzzle":
    //            ChangeMuzzle(levelValue);
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //public void ChangeScope(int levelValue)
    //{
    //    for (int i = 0; i < scopes.Length; i++)
    //    {
    //        scopes[i].gameObject.SetActive(false);
    //    }
    //    scopes[levelValue].gameObject.SetActive(true);
    //    currentScope = levelValue;
    //    SaveManager.instance.SetGunParts(currentScope,
    //                                     SaveManager.instance.GetGrip(),
    //                                     SaveManager.instance.GetLaser(),
    //                                     SaveManager.instance.GetMuzzle());
    //}
    //public void ChangeGrip(int levelValue)
    //{
    //    for (int i = 0; i < grips.Length; i++)
    //    {
    //        grips[i].gameObject.SetActive(false);
    //    }
    //    grips[levelValue].gameObject.SetActive(true);
    //    currentGrip = levelValue;
    //    SaveManager.instance.SetGunParts(SaveManager.instance.GetScope(),
    //                                     currentGrip,
    //                                     SaveManager.instance.GetLaser(),
    //                                     SaveManager.instance.GetMuzzle());
    //}
    //public void ChangeLaser(int levelValue)
    //{
    //    for (int i = 0; i < lasers.Length; i++)
    //    {
    //        lasers[i].gameObject.SetActive(false);
    //    }
    //    lasers[levelValue].gameObject.SetActive(true);
    //    currentLaser = levelValue;
    //    SaveManager.instance.SetGunParts(SaveManager.instance.GetScope(),
    //                                     SaveManager.instance.GetGrip(),
    //                                     currentLaser,
    //                                     SaveManager.instance.GetMuzzle());
    //}
    //public void ChangeMuzzle(int levelValue)
    //{
    //    for (int i = 0; i < muzzles.Length; i++)
    //    {
    //        muzzles[i].gameObject.SetActive(false);
    //    }
    //    muzzles[levelValue].gameObject.SetActive(true);
    //    currentMuzzle = levelValue;
    //    SaveManager.instance.SetGunParts(SaveManager.instance.GetScope(),
    //                                     SaveManager.instance.GetGrip(),
    //                                     SaveManager.instance.GetLaser(),
    //                                     currentMuzzle);
    //}
}