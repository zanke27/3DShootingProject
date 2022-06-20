using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class PlayerGun : AgentGun
{
    public UnityEvent OnReloading;
    private PlayerMove _playerMove = null;

    public bool IsReloading
    {
        get => _gun.IsReloading;
        set => _gun.IsReloading = value;
    }

    public bool CanReloading => !IsReloading && _gun.Ammo<_gun.WeaponData.ammoCapacity && _playerMove.IsRun == false;

    protected override void AwakeChild()
    {
        _playerMove = Player.GetComponent<PlayerMove>();
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
