using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("SO/Weapon"))]
public class WeaponDataSO : ScriptableObject
{
    public GameObject gunPrefab;
    //public BulletDataSO bulletData; // �ϴ� �̰� �׳� �Ѿ� �ϳ��� ������ ����

    [Range(0, 999)] public int ammoCapacity = 24; // �Ѿ� ��
    public bool automaticfire; // �� ������ ��������

    [Range(0.1f, 2f)] public float weaponDelay = 0.1f; // �� ������
    [Range(0, 10f)] public float spreadX = 1f;
    [Range(0, 10f)] public float spreadY = 1f;

    [SerializeField] private bool _multiBulletShoot = false; // �ѹ��� ������ ����? ex: ����
    [SerializeField] public int _bulletCount = 1; // ���߹߻簡���ϸ� ������ ��� �ƴϸ� �����

    public int damageFactor = 1; // �� ������ ����

    [Range(1f, 5f)] public float reloadTime = 2f; // ������ �ð�

    public AudioClip shootClip;
    public AudioClip noAmmoClip;
    public AudioClip reloadClip;

    public Sprite panelSprite;

    public int GetBulletCountToSpawn()
    {
        return _multiBulletShoot ? _bulletCount : 1;
    }


}
