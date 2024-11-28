using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trapTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] traps;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < traps.Length; i++)
        {

            ProjectileLauncher state = traps[i].GetComponent<ProjectileLauncher>();

            if (state != null)
            {
                state.setRun(!state.run);
            }
        }
    }
}
