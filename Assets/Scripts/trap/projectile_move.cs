using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_move : MonoBehaviour
{
    // Start is called before the first frame update
    public float aliveTime = 5f;
    public float moveSpeed = 5f;
    private float timer = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
        timer += Time.deltaTime;

        if(timer >= aliveTime)
        {

            Destroy(gameObject);
        }
    }


   

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.collider.CompareTag("Player"))
        {

            print("HIT");
            Destroy(gameObject);
        }

        if (collision.collider.CompareTag("Ground"))
        {
            print("GOT HERE");
            Destroy(gameObject);
        }
        
    }

}
