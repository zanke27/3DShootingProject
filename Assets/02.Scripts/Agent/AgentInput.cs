using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [field: SerializeField] public UnityEvent<Vector3> OnMovementKeyPress;
    [field: SerializeField] public UnityEvent<Vector3> OnPointerPositionChanged;

    public UnityEvent OnReloadButtonPress;
    private bool _fireButtonDown = false;

    public UnityEvent OnTiltLeftButtonPress;
    public UnityEvent OnTiltRightButtonPress;

    public UnityEvent OnRunButtonPress;
    public UnityEvent OnRunButtonRelease;

    public UnityEvent OnCrouchButtonPress;

    [Header("마우스 클릭")]
    [field: SerializeField] public UnityEvent OnFireButtonPress;
    [field: SerializeField] public UnityEvent OnFireButtonRelease;

    public UnityEvent OnAimmingButtonPress;
    public UnityEvent OnAimmingButtonRelease;

    private void Update()
    {
        GetFireButton();
        GetMouseRightButton();
        GetReloadButton();
        GetRunButton();
        GetTiltButton();
        GetCrouchButtonPress();
    }

    private void GetRunButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnRunButtonPress?.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnRunButtonRelease?.Invoke();
        }
    }

    private void GetCrouchButtonPress()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            OnCrouchButtonPress?.Invoke();
        }
    }

    private void GetTiltButton()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnTiltLeftButtonPress?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            OnTiltRightButtonPress?.Invoke();
        }
    }

    private void GetReloadButton()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnReloadButtonPress?.Invoke();
        }
    }

    private void GetFireButton()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if(_fireButtonDown == false)
            {
                _fireButtonDown = true;
                OnFireButtonPress?.Invoke();
            }
        }
        else
        {
            if(_fireButtonDown == true)
            {
                _fireButtonDown = false;
                OnFireButtonRelease?.Invoke();
            }
        }
    }

    private void GetMouseRightButton()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnAimmingButtonPress?.Invoke();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            OnAimmingButtonRelease?.Invoke();
        }
    }
}
