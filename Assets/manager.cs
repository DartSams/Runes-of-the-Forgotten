using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manager : MonoBehaviour
{
    GameObject player;
    public GameObject startingPos;
    public RuntimeAnimatorController anim2;
    Camera cam;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("player");
        cam = Camera.main;
        player.transform.position = startingPos.transform.position;

        //Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Sutton-level-new")
        {
            player.GetComponent<PlayerControllerManual>().enabled = true;
            player.GetComponent<MainPlayerScript>().enabled = false;
            player.GetComponent<PlayerControllerManual>().GameOver = GameObject.FindWithTag("manager").GetComponent<ManageGameOver>();
            //change player scale
            player.transform.localScale = new Vector3(0.650848f, 0.650848f, 1);
            Animator animator = player.GetComponent<Animator>();
            animator.runtimeAnimatorController = anim2;
        }

        if (SceneManager.GetActiveScene().name == "RunesLevel")
        {
            player.GetComponent<PlayerControllerManual>().moveSpeed = 15;
            player.GetComponent<PlayerControllerManual>().jumpForce = 15;
            player.transform.localScale = new Vector3(3.8f, 3.8f, 3.8f);
            cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,-10f);
            cam.GetComponent<Camera>().orthographicSize = 8.985659f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
