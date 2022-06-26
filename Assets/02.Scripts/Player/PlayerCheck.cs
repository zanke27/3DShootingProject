using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerCheck : MonoBehaviour
{
    public CapsuleCollider playerCol;
    public Image hurtImage;
    public CanvasGroup canvasGroup;


    private float hpUpTime = 3;
    private float checkTime = 0;
    private bool isHit = false;
    private float hitTimeCheck = 0;

    private int hp = 100;

    public UnityEvent PlayerDieEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            if (isHit == true) return;
            hp -= 20;

            PlayerHit();
            isHit = true;
            hitTimeCheck = 0;
            if (hp <= 0)
            {
                PlayerDieEvent?.Invoke();
            }
        }
    }

    private void Update()
    {
        hitTimeCheck += Time.deltaTime;
        if (hitTimeCheck >= 1 && isHit == true)
        {
            isHit = false;
        }
        checkTime += Time.deltaTime;
        if (checkTime >= hpUpTime)
        {
            checkTime = 0;
            PlayerHpUp();
        }
    }

    private void PlayerHpUp()
    {
        if (canvasGroup.alpha <= 0) return;
        canvasGroup.alpha -= 0.15f;
        hp += 20;
        if (canvasGroup.alpha <= 0)
        {
            //hurtImage.gameObject.SetActive(false);
        }
    }

    private void PlayerHit()
    {
        //hurtImage.gameObject.SetActive(true);
        canvasGroup.alpha += 0.15f;
        checkTime = 0;
    }

    //IEnumerator InvincibilityCoroutine()
    //{
    //    hitTimeCheck = 0;
    //    while(true)
    //    {
    //        hitTimeCheck += Time.deltaTime;
    //        if (hitTimeCheck >= 1)
    //        {
    //            hitTimeCheck = 0;
    //            isHit = false;
    //            break;
    //        }
    //    }
    //    yield return null;
    //}
}
