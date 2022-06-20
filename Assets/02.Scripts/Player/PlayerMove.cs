using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 2f;
    public float walkSpeed = 2f;
    public float crouchSpeed = 1f;
    public float runSpeed = 3f;
    public float rotationSpeed = 10f;
    public GameObject camPos;

    private Rigidbody _rigidbody;
    private float rx;
    private float ry;

    public float spread;
    private bool isCrouch = false;
    private bool isRun = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Run()
    {
        if (isRun)
        {
            isRun = false;
            speed = walkSpeed;
        }
        else if (!isRun)
        {
            isRun = true;
            speed = runSpeed;
        }
    }

    public void Crouch()
    {
        if (isCrouch)
        {
            isCrouch = false;
            speed = walkSpeed;
        }
        else if (!isCrouch)
        {
            isCrouch = true;
            speed = crouchSpeed;
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = (camPos.transform.forward * z) + (camPos.transform.right * x);

        moveDir.Normalize();
        moveDir.y = 0f;

        _rigidbody.velocity = moveDir * speed;

        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        ry = rotationSpeed * mx;
        rx += rotationSpeed * my;

        rx = Mathf.Clamp(rx, -80f, 80f);

        transform.Rotate(new Vector3(0f, ry, 0f),Space.Self);

        camPos.transform.localEulerAngles = new Vector3(-rx, 0, 0);
    }

}
