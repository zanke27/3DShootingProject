using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("SO/Weapon"))]
public class WeaponDataSO : ScriptableObject
{
    public GameObject gunPrefab;
    //public BulletDataSO bulletData; // 일단 이건 그냥 총알 하나로 나가게 하자

    [Range(0, 999)] public int ammoCapacity = 24; // 총알 수
    public bool automaticfire; // 꾹 누르면 나가는지

    [Range(0.1f, 2f)] public float weaponDelay = 0.1f; // 총 딜레이
    [Range(0, 10f)] public float spreadX = 1f;
    [Range(0, 10f)] public float spreadY = 1f;

    [SerializeField] private bool _multiBulletShoot = false; // 한번의 여러발 가능? ex: 샷건
    [SerializeField] public int _bulletCount = 1; // 다중발사가능하면 여러발 쏘고 아니면 점사로

    public int damageFactor = 1; // 총 데미지 배율

    [Range(1f, 5f)] public float reloadTime = 2f; // 재장전 시간

    public AudioClip shootClip;
    public AudioClip noAmmoClip;
    public AudioClip reloadClip;

    public Sprite panelSprite;

    public int GetBulletCountToSpawn()
    {
        return _multiBulletShoot ? _bulletCount : 1;
    }


}
