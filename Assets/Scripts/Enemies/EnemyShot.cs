using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public static EnemyShot instance;

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject laser;
    [SerializeField] float shotForce = 1500f;
    [SerializeField] float shotRate = 0.5f;

    private float shotRateTime = 0;
    public bool playerFound = false;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (playerFound)
        {
            if (Time.time > shotRateTime)
            {
                GameObject newLaser;

                newLaser = Instantiate(laser, spawnPoint.position, spawnPoint.rotation);
                newLaser.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shotForce);
                shotRateTime = Time.time + shotRate;

                //Despues de 5 segundos se destruye
                Destroy(newLaser, 5);
            }
        }        
    }
}
