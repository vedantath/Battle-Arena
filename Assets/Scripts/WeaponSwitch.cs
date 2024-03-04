using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public static int selectedWeapon = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int prevSelectedWeapon = selectedWeapon;

       if(Input.GetAxis("Mouse ScrollWheel") > 0f) //scroll up
       {
            if(selectedWeapon >= transform.childCount-1)
                selectedWeapon = 0;
            else 
                selectedWeapon++;
       } 
       if(Input.GetAxis("Mouse ScrollWheel") < 0f) //scroll down
       {
            if(selectedWeapon <= 0)
                selectedWeapon = transform.childCount-1;
            else 
                selectedWeapon--;
       } 
        //Key Input
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }





       if(prevSelectedWeapon != selectedWeapon)
       {
            SelectWeapon();
       }
       


    
    }

    void SelectWeapon() {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            i++;
        }
    }

    public static int getSelectedWeapon() { return selectedWeapon;}
}
