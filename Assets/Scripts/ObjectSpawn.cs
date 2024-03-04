using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{

    public GameObject objToSpawn;
    public float timer = 0.0f;

    void Start()
    {
        timer = Time.time;
        
    }

    // Update is called once per frame
    void Update()
    {
       if (objToSpawn != null && Time.time - timer > 30) {
            Spawn(Random.Range(24, 51), 40f, Random.Range(4, 99), objToSpawn);
            timer = Time.time;
        } 
    }
    void Spawn(float x, float y, float z, GameObject obj)
    {
        Vector3 spawnPoint  = new Vector3(x, y, z);
        GameObject newObj = Instantiate(obj, spawnPoint, Quaternion.identity) as GameObject;
    }
}
