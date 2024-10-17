using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class death : MonoBehaviour
{   
    public PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        // Check if the other object has a PlayerController component
        if (other.GetComponent<PlayerController>() != null)
        {

            
            // Destroy the collectible
            Destroy(controller);


        }


    }
}
