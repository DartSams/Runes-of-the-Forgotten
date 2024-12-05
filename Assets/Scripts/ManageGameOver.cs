using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverMenu;
    public string menuSceneName;
    public string levelSelectSceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toMainMenu()
    {

        SceneManager.LoadScene(menuSceneName);
        
    }

    public void toLevelSelect()
    {
        SceneManager.LoadScene(levelSelectSceneName);
    }


    public void gameOver()
    {

        gameOverMenu.SetActive(true);
    }

    public void Restart()
    {

        SceneManager.LoadScene("OakWoodsLevel");

    }
}
