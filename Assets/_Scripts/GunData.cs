using UnityEngine;

[CreateAssetMenu(fileName = "Gun" , menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    public Bullet bullet;

    [Header("Info")]
    public new string name;
    public string color;
    public int muzzleCount;

    [Header("Shooting")]
    public int damage;
    public float fireRate;
    public float maxDistance;
}