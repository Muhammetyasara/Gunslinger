using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Gun gun;
    //public GunParts gunParts;

    public ParticleSystem buffEffect;

    public bool isGunChange;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //gunParts = gun.GetComponent<GunParts>();
    }
    private void Start()
    {
        gun.gameObject.SetActive(true);
    }
}