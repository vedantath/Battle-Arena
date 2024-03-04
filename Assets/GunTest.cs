using UnityEngine.Events;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    public UnityEvent onGunShoot;
    public float FireCooldown;
    public bool Automatic;
    private float CurrentCooldown;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if(Automatic)
        {
            if(Input.GetMouseButton(0))
            {
                if(CurrentCooldown <= 0f)
                {
                    onGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }
        else
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(CurrentCooldown <= 0f)
                {
                    onGunShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }

        CurrentCooldown = Time.deltaTime;
    }
}
