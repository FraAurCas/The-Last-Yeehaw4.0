using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Melee : ScriptableObject, IWeapon
{
    public float attackCooldown;
    public int damage;

    float IWeapon.AttackCooldown => attackCooldown;
    int IWeapon.Damage => damage;
    public abstract void attack(WeaponController weapon);
}
