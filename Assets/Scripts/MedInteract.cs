using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IHeal {
    public void Heal();
}
public class MedInteract : MonoBehaviour, IInteractable
{

    public GameObject obj;
    public GameObject player;
    public void Interact() {
        obj.SetActive(false);
        if(player.TryGetComponent(out IHeal healObj))
            healObj.Heal();
        
    }

   /* void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
