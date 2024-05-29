using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    public static DDOL instance;

    public List<Transform> currentGunParts = new List<Transform>();
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }
}
