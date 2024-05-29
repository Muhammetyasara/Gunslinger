using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public Gun gun;
    public RocketLauncher rocketLauncher;
    public GameObject currentGun;

    Touch touch;
    Vector3 newPos;
    Vector3 newRot;
    Vector3 clampPos;

    public float moveSpeed;
    public float rotSpeed;
    public float forwardSpeed;
    public float positiveXClampValue;
    public float negativeXClampValue;
    public bool isTouchingEnded;
    public bool isTriggerGate;
    public bool isUpgrade;

    private void Start()
    {
        if (GamePlayManager.instance.isMoneyRush)
        {
            currentGun = rocketLauncher.gameObject;
        }
        else
        {
            currentGun = Player.instance.gun.gameObject;
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.isLevelStarted && !isUpgrade)
        {
            //TouchMove();
            RotateTheGun();
            PlayerClamp(positiveXClampValue, negativeXClampValue);
        }
    }
    private void Update()
    {
        if (GameManager.instance.isLevelStarted && !isUpgrade)
        {
            MouseMove();
        }
        if (isUpgrade)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(4, 1, transform.position.z), Time.deltaTime * 2);
            Camera.main.transform.DOLocalMoveX(0, 1f);
            currentGun.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(currentGun.transform.eulerAngles.z, 0f, .1f));
            newRot = currentGun.transform.localEulerAngles;
        }
    }

    public void MouseMove()
    {
        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;

        float horizontalMovement = Input.GetAxis("Mouse X");
        if (Input.GetMouseButton(0))
        {
            isTouchingEnded = false;

            transform.Translate(horizontalMovement * Time.deltaTime * moveSpeed, 0, 0);
            if (horizontalMovement < 0)
            {
                newRot = currentGun.transform.localEulerAngles;
                newRot.z += Input.mousePosition.x * Time.deltaTime * rotSpeed;

                currentGun.transform.localEulerAngles = newRot;
            }
            else if (horizontalMovement > 0)
            {
                newRot = currentGun.transform.localEulerAngles;
                newRot.z -= Input.mousePosition.x * Time.deltaTime * rotSpeed;

                currentGun.transform.localEulerAngles = newRot;
            }
            else
            {
                currentGun.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(currentGun.transform.eulerAngles.z, 0f, .1f));
                newRot = currentGun.transform.localEulerAngles;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouchingEnded = true;
        }

    }
    public void TouchMove()
    {
        transform.position += Vector3.forward * Time.deltaTime * forwardSpeed;

        if (Input.touchCount > 0)
        {
            isTouchingEnded = false;

            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                newPos = transform.position;
                newPos.x += touch.deltaPosition.x * Time.deltaTime * moveSpeed;
                transform.position = newPos;

                if (!isTriggerGate)
                {
                    newRot = currentGun.transform.localEulerAngles;
                    newRot.z -= touch.deltaPosition.x * Time.deltaTime * rotSpeed;

                    currentGun.transform.localEulerAngles = newRot;
                }
            }
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Ended)
            {
                isTouchingEnded = true;
            }
        }
    }
    public void RotateTheGun()
    {
        if (isTouchingEnded && !isTriggerGate)
        {
            currentGun.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(currentGun.transform.eulerAngles.z, 0f, .1f));
            newRot = currentGun.transform.localEulerAngles;
        }
    }

    public void PlayerClamp(float positiveXClamp, float negativeXClamp)
    {
        clampPos = transform.position;
        clampPos.x = Mathf.Clamp(clampPos.x, negativeXClamp, positiveXClamp);
        transform.position = clampPos;
    }
}