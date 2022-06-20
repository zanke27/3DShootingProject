using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;
using Random = UnityEngine.Random;

public class Gun : MonoBehaviour
{
    #region 총 관련 데이터
    [SerializeField] private WeaponDataSO _weaponData;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private GameObject _aimMuzzle;
    [SerializeField] private bool _isEnemyWeapon;
    public WeaponDataSO WeaponData => _weaponData;
    private bool _isAimming = false;
    public bool IsAimming
    {
        get => _isAimming;
        set => _isAimming = value;
    }
    private bool _isReloading = false;
    public bool IsReloading
    {
        get => _isReloading;
        set => _isReloading = value;
    }
    #endregion

    [SerializeField] private GameObject bulletPrefab;

    public UnityEvent<int> OnAmmoChange;
    
    [SerializeField] private int _ammo; // 현재 총알 수
    public int Ammo
    {
        get => _ammo;
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
            OnAmmoChange?.Invoke(_ammo);
        }
    }

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    private bool _isShooting = false;
    [SerializeField] private bool _delayCoroutine = false;

    private PlayerMove _playerMove = null;

    private void Awake()
    {
        _ammo = _weaponData.ammoCapacity;
        _playerMove = Player.GetComponent<PlayerMove>();
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (_isShooting == true && _delayCoroutine == false && _isReloading == false && _playerMove.IsRun == false)
        {
            if(Ammo > 0)
            {
                Ammo -= _weaponData.GetBulletCountToSpawn();
                OnShoot?.Invoke();
                ShootRebound();
                for (int i = 0; i < _weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                _isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void ShootRebound()
    {
        MainCam.transform.rotation *= Quaternion.Euler(-_weaponData.spreadX, 0f, 0f);
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());

        if(_weaponData.automaticfire == false)
        {
            _isShooting = false;
        }
    }

    private IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCoroutine = false;
    }
                
    private void ShootBullet()
    {
        Vector3 pos = _isAimming == true ? _aimMuzzle.transform.position : _muzzle.transform.position;
        Quaternion rot = _isAimming == true ? _aimMuzzle.transform.rotation : _muzzle.transform.rotation;

        SpawnBullet(pos, rot, _isEnemyWeapon);
    }

    private void SpawnBullet(Vector3 pos, Quaternion rot, bool isEnemyBullet)
    {
        Instantiate(bulletPrefab, pos, rot, null);
    }

    public void TryShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }

    public Vector3 GetFirePosition() => _muzzle.transform.position;

    public void AimmingCheck()
    {
        _isAimming = true;
    }

    public void DeAimmingCheck()
    {
        _isAimming = false;
    }

    public void ResetWeapon()
    {
        _isShooting = false;
        _delayCoroutine = false;
    }
}
