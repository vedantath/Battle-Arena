using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController playerHeight;
    public CapsuleCollider playerCol;
    public float normalHeight, crouchHeight;
    public Transform player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        #region Handle Crouch
        if(Input.GetKeyDown(KeyCode.C)){
            playerHeight.height = crouchHeight;
            playerCol.height = crouchHeight;
        }

        if(Input.GetKeyUp(KeyCode.C)){
            playerHeight.height = normalHeight;
            playerCol.height = normalHeight;
            player.position += offset;
        }

        #endregion
    }

}
