using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentGun : MonoBehaviour
{
    [NonSerialized] public Gun _gun;

    protected void Awake()
    {
        AssignWeapon();
        AwakeChild();
    }

    protected virtual void AwakeChild()
    {

    }

    public virtual void AssignWeapon()
    {
        _gun = GetComponentInChildren<Gun>();
    }

    public virtual void Shoot()
    {
        _gun.TryShooting();
    }

    public virtual void StopShooting()
    {
        _gun.StopShooting();
    }
}
