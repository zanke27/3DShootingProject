using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    private static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null)
            {
                _mainCam = Camera.main;
            }
            return _mainCam;
        }
    }

    private static GameObject _player = null;
    public static GameObject Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.Find("Player").gameObject;
            }
            return _player;
        }
    }
}
