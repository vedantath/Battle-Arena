using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    public GameObject enemy;
    public float timer = 0.0f;
    void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy != null && Time.time - timer > 10) {
            Spawn(Random.Range(24, 51), 15.5f, Random.Range(4, 99), enemy);
            timer = Time.time;
        }
    }
    
    void Spawn(float x, float y, float z, GameObject obj)
    {
        Vector3 spawnPoint  = new Vector3(x, y, z);
        GameObject newEnemy = Instantiate(obj, spawnPoint, Quaternion.identity) as GameObject;
    }
}
