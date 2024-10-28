using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class trapTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] traps;
    public bool activate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].SetActive(activate);
        }
    }
}
