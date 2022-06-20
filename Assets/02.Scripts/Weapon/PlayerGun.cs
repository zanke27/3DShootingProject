using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGun : AgentGun
{
    public UnityEvent OnReloading;

    public bool IsReloading
    {
        get => _gun.IsReloading;
        set => _gun.IsReloading = value;
    }

    public bool CanReloading => !IsReloading && _gun.Ammo<_gun.WeaponData.ammoCapacity;

    protected override void AwakeChild()
    {

    }

    public void Reloading()
    {
        if (CanReloading)
        {
            StopShooting();
            IsReloading = true;
            OnReloading?.Invoke();
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(_gun.WeaponData.reloadTime);
        _gun.Ammo = _gun.WeaponData.ammoCapacity;
        IsReloading = false;
    }

}
