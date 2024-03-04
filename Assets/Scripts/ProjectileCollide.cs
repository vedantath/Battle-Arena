using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileCollide : MonoBehaviour
{

    GameObject objToDestroy;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("proj hit wall");
        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    
    }
}
