using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour {

    public RectTransform reticle; // The RecTransform of reticle UI element.

    public float restingSize;
    public float maxSize;
    public float speed;
    public float currentSize;

    //float defCurrentSize;

   // public Camera playerCamera;

    //public float cameraZoom;
    public GameObject reticleToggle;
    public Reticle()
    {
    
    }

    private void Start() {

        reticle = GetComponent<RectTransform>();
        //defCurrentSize = currentSize;

    }

    private void Update() {

        int weaponSelected = WeaponSwitch.getSelectedWeapon();
        if(weaponSelected == 1)
        {
            reticleToggle.SetActive(false);
        }
        else{
            reticleToggle.SetActive(true);
        }
        //reticleToggle.SetActive(true);

        // Check if player is currently moving and Lerp currentSize to the appropriate value.
        if (isMoving) {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        } else {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        if(Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1))
        {
            currentSize = Mathf.Lerp(currentSize, 10, Time.deltaTime * speed);
        }


        // Set the reticle's size to the currentSize value.
        reticle.sizeDelta = new Vector2(currentSize, currentSize);


    }

    public float getCurrentSize(){
        return currentSize;
    }

    // Bool to check if player is currently moving.
    bool isMoving {

        get {

            // If we have assigned a rigidbody, check if its velocity is not zero. If so, return true.
            /*if (playerRigidbody != null)
                if (playerRigidbody.velocity.sqrMagnitude != 0)
                    return true;
                else
                    return false;
                */
            // If not rigidbody is assigned, check Input axis' instead.
            if (
                Input.GetAxis("Horizontal") != 0 ||
                Input.GetAxis("Vertical") != 0 ||
                Input.GetAxis("Mouse X") != 0 ||
                Input.GetAxis("Mouse Y") != 0
                    )
                return true;
            else
                return false;

        }

    }



}
