using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : ScriptableObject, IWeapon
{
    public int currentAmmo;
    public int damage;
    public int totalAmmo;
    public float firingCooldown;
    public float reloadTime;
    public int clipSize;

    float IWeapon.AttackCooldown => firingCooldown;
    int IWeapon.Damage => damage;

    public abstract void attack(WeaponController weapon);
}

