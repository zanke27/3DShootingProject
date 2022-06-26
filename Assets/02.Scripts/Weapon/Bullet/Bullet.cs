using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private LayerMask enemyLayer;

    private void Start()
    {
        Invoke("DestroyObj", 2f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
