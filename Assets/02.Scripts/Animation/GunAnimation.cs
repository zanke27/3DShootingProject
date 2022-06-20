using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimation : MonoBehaviour
{
    #region 애니메이션 관련
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private GameObject _idlePos;
    [SerializeField] private GameObject _aimPos;
    private readonly int _shootHashStr = Animator.StringToHash("Shoot");
    private readonly int _runHashStr = Animator.StringToHash("Run");
    private readonly int _reloadHashStr = Animator.StringToHash("Reload");
    private readonly int _reloadNoAmmoHashStr = Animator.StringToHash("ReloadNoAmmo");
    #endregion
    private PlayerGun _playerGun = null;

    private void Awake()
    {
        _playerGun = GetComponentInParent<PlayerGun>();
        _gunAnimator = GetComponentInChildren<Animator>();
    }

    public void RunAnimation()
    {
        if (_playerGun.IsReloading == true) return;
        if (_gunAnimator.GetBool("Run") == true)
        {
            _gunAnimator.SetBool(_runHashStr, false);
        }
        else
        {
            _gunAnimator.SetBool(_runHashStr, true);
        }
    }

    public void ShootAnimation()
    {
        if (_playerGun.IsReloading == true) return;
        if (_gunAnimator.GetBool("Aim") == true)
        {
            _gunAnimator.SetTrigger(_shootHashStr);
        }
        else
        {
            _gunAnimator.SetTrigger(_shootHashStr);
        }
    }

    public void ReloadAnimation()
    {
        if (_playerGun.CanReloading) return;
        _gunAnimator.SetFloat("Time", 1 / _playerGun._gun.WeaponData.reloadTime);
        if (_playerGun._gun.Ammo == 0)
        {
            _gunAnimator.SetTrigger(_reloadNoAmmoHashStr);
        }
        else
        {
            _gunAnimator.SetTrigger(_reloadHashStr);
        }
    }

    #region 조준, 비조준 함수
    public void Aimming()
    {
        _gunAnimator.SetBool("Aim", true);
        transform.position = _aimPos.transform.position;
        _playerGun._gun.IsAimming = true;
    }

    public void DeAimming()
    {
        _gunAnimator.SetBool("Aim", false);
        transform.position = _idlePos.transform.position;
        _playerGun._gun.IsAimming = false;
    }
    #endregion

}
