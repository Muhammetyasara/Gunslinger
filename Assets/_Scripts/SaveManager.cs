using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public int gunIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetGunIndex(int value)
    {
        PlayerPrefs.SetInt(nameof(gunIndex), value);
    }
    public int GetGunIndex()
    {
       return PlayerPrefs.GetInt(nameof(gunIndex));
    }
    
    //public int scope;
    //public int grip;
    //public int laser;
    //public int muzzle;
    //public void SetGunParts(int scopeValue, int gripValue, int laserValue, int muzzleValue)
    //{
    //    PlayerPrefs.SetInt(nameof(scope), scopeValue);
    //    PlayerPrefs.SetInt(nameof(grip), gripValue);
    //    PlayerPrefs.SetInt(nameof(laser), laserValue);
    //    PlayerPrefs.SetInt(nameof(muzzle), muzzleValue);
    //}
    //public int GetScope()
    //{
    //   return PlayerPrefs.GetInt(nameof(scope));
    //}
    //public int GetGrip()
    //{
    //  return  PlayerPrefs.GetInt(nameof(grip));
    //}
    //public int GetLaser()
    //{
    //  return  PlayerPrefs.GetInt(nameof(laser));
    //}
    //public int GetMuzzle()
    //{
    //   return PlayerPrefs.GetInt(nameof(muzzle));
    //}
}