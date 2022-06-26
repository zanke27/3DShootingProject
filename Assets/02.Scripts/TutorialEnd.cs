using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialEnd : MonoBehaviour
{
    public UnityEvent GoToMain;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GoToMain?.Invoke();
        }
    }
}
