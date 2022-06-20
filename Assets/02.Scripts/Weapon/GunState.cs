using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : MonoBehaviour
{
    private int gunIndex = 0;
    
    [SerializeField]
    private bool _isAttack = false;
    public bool IsAttack
    {
        get => _isAttack;
        set
        {
            if (value == true && gunIndex == 0)
            {
                _isAttack = true;
                gunIndex++;
            }
            else if (value == true && gunIndex >= 1)
                gunIndex++;
            else if (value == false && gunIndex == 1)
            {
                _isAttack = false;
                gunIndex--;
            }
            else if (value == false && gunIndex > 1)
                gunIndex--;
        }
    }

    private bool _isReloading = false;
    public bool IsReloading
    {
        get => _isReloading;
        set { _isReloading = value; }
    }

}
