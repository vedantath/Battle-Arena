using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    public Rigidbody my_Rigidbody;
    CharacterController characterController;
    public Camera playerCamera;
    
    public float maxCameraZoom;
    private float currentCameraSize;
    public float CamSpeed;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXlimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    float defeaultMax;

    //Image scope;
    public GameObject ScopeCam;

    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentCameraSize = 80;

        ScopeCam.SetActive(false);

        defeaultMax = maxCameraZoom;
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //Press Left Shift to Run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles FOV
        if(Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1))
        {
            currentCameraSize = Mathf.Lerp(currentCameraSize, maxCameraZoom, Time.deltaTime * CamSpeed);
        }
        if(!(Input.GetKey(KeyCode.Q)) && !(Input.GetMouseButton(1)))
        {
            currentCameraSize = 80;
        }

        playerCamera.fieldOfView = currentCameraSize;
        #endregion

        #region Handles Jumping
        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXlimit, lookXlimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        #region Handle Scope Cam

        int weaponSelected = WeaponSwitch.getSelectedWeapon();
        if(weaponSelected == 1)
        {
            maxCameraZoom = 10;
            if(Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1))
            {
                ScopeCam.SetActive(true);
                lookSpeed = 0.3f;
                currentCameraSize = Mathf.Lerp(currentCameraSize, maxCameraZoom, Time.deltaTime * CamSpeed);
            }
            if(!Input.GetKey(KeyCode.Q) && !Input.GetMouseButton(1))
            {
                ScopeCam.SetActive(false);
                currentCameraSize = 80;
                maxCameraZoom= defeaultMax;
                lookSpeed = 2f;
            }

            playerCamera.fieldOfView = currentCameraSize;
        }

        #endregion
    }
}
