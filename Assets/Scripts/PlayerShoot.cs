using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public static Action shootInput;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
    }
    


}
