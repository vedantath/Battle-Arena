//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using TMPro;
//using Unity.PlasticSCM.Editor.WebApi;
//using System;

public class ProjectileGun : MonoBehaviour
{
    
    public GameObject bullet;
    //private RectTransform reticle;
    public float shootForce, upwardForce;
    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public float spread, range;
    public int magSize, bulletsPerTap;
    public bool allowButtonHold;
    public int damage;

    int bulletsLeft, bulletsShot;
    

    bool shooting, readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public ParticleSystem muzzleFlash;
    public ParticleSystem cartridgeEject;
    public TextMeshProUGUI ammoDisplay;
    public GameObject hitMarker;
    


    //public float restingSize;
    //public float maxSize;
    //public float speed;
    //public float currentSize;

    //bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure mag is full
        //Reticle reticle = new Reticle();
        bulletsLeft = magSize;
        readyToShoot = true;
        //hitMarker = GetComponent<GameObject>();
        hitMarker.SetActive(false);
        //spread = reticle.getCurrentSize();
    }

   /* private void Start(){
        ammoDisplay = GetComponent<TextMeshProUGUI>();
    }*/

    private void Update()
    {
       myInput(); 

       //Set ammo display
       if(ammoDisplay != null) {
            if(reloading)
            {
                ammoDisplay.SetText("Reloading");
            }
            else if (!reloading)
                ammoDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magSize / bulletsPerTap);
       }
    }

    private void myInput()
    {
        //Check if allowed to hold down fire btn and take input
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading) Reload();
        //Reload automatically when trying to shoot w/out ammo
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();


        //Shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Debug.Log("class:ProjectileGun.90 --> Shoot()");
        //RayCast 11/30/23 --> Checking ray collide w/ enemy & do damage
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name+" - class: ProjectileGun.97");
            
            if(rayHit.collider.CompareTag("Enemy"))
            {
                //rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage); //create script w/ TakeDamage function
                rayHit.collider.GetComponent<EnemyDamage>().takeDamage(damage);
                hitMarker.SetActive(true);
            }
        }



        //find exact hit position using RayCast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //ray through middle of screen
        RaycastHit hit;


        //check if ray hits obj
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //point far away from player

        //Calculate direction from attacPt to TargetPt
        
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;
    
        //test
        /*if (isMoving) {
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
        } else {
            currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
        }

        if(Input.GetKey(KeyCode.Q) || Input.GetMouseButton(1))
        {
            currentSize = Mathf.Lerp(currentSize, 10, Time.deltaTime * speed);
        }

        spread = currentSize;*/

        //calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        //float x = Random.Range(0, spread-40);
        //float y = Random.Range(0, spread-40);
        //w

        //float x = Random.Range(-reticle.getCurrentSize(), );
        //float y = Random.Range();

        //Calculate new direction w/ spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate bullet
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //spawned bullet stored here
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash
       // if(muzzleFlash != null)
           // Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        muzzleFlash.Play();
        cartridgeEject.Play();

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot func (if not already)
        if(allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            //Destroy(currentBullet);
            allowInvoke = false;
        }
        //if multiple bulletsPerTap make sure to repeat shoot func
        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        //hitMarker.SetActive(false);
        //2-12-2024
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        hitMarker.SetActive(false);
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }

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

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            
        }

    }*/

}
