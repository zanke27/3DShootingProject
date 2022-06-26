using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RemyController : MonoBehaviour
{
    public UnityEvent Rescue;

    bool isGo = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isGo) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            isGo = true;
            Rescue?.Invoke();
        }
    }
}
