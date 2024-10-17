using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arror : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform spawnLocation;
    public Quaternion spawnRotation;
    public float spawnTime = 0.5f;

    private float timeSinceSpawned = 0f;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceSpawned+= Time.deltaTime;

        if(timeSinceSpawned >= spawnTime)
        {
            Instantiate(projectile, spawnLocation.position, spawnRotation);
            timeSinceSpawned = 0;

        }
       

    }

    

    
}
