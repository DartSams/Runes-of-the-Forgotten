using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform spawnLocation;
    public Quaternion spawnRotation;
    public float spawnTime = 0.5f;
    public Boolean run = false;
    private float timeSinceSpawned = 1f;
    
    
    public void setRun(Boolean run)
    {

        this.run = run;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (run)
        {

            timeSinceSpawned += Time.deltaTime;

            if (timeSinceSpawned >= spawnTime)
            {
                GameObject arrow = Instantiate(projectile, spawnLocation.position, spawnRotation);
                arrow.transform.Rotate(0, 0, 90f);
                timeSinceSpawned = 0;



            }
        }
       

    }

    

    
}
