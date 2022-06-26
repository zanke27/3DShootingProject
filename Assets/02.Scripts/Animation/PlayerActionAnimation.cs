using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerActionAnimation : MonoBehaviour
{
    private Animator _actionAnimator = null;
    private PlayerMove _playerMove = null;

    private void Awake()
    {
        _actionAnimator = GetComponent<Animator>();
        _playerMove = Player.GetComponent<PlayerMove>();
    }

    public void Crouch()
    {
        if (_playerMove.IsRun) return;
            _actionAnimator.SetBool("Crouch", _playerMove.IsCrouch);
    }
}
