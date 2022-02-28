using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public float AttackCooldown { get; }
    public int Damage { get; }
    public void attack(WeaponController weapon);

}
