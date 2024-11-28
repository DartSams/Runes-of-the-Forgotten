
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class persistentManager : MonoBehaviour
{
    public static persistentManager instance;
    public int totalCoins;
    public TMP_Text enemyCountText;
    public TMP_Text playerHealthText;
    int enemyCount;
    GameObject[] enemies;
    public List<GameObject> runeImages;
    int currRuneIndex = 0;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindWithTag("player");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyCount = enemies.Length;
        enemyCountText.text = "Enemies: " + enemyCount.ToString();
        playerHealthText.text = "Player Health: " + player.gameObject.GetComponent<dartagnanplayerScript>().currentHealth;
        UpdateEnemyCount();
    }

    public void AddCoins(int coins)
    {
        totalCoins += coins;
    }
    
    public void UpdateEnemyCount()
    {
        enemyCount--;
        enemyCountText.text = "Enemies: " + enemyCount.ToString();
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        enemyCount = enemies.Length;
        if (enemyCount <= 0)
        {
            GameObject rune = instance.runeImages[currRuneIndex];


            Image image = rune.GetComponent<Image>();

            Color color = image.color;
            color.a = Mathf.Clamp01(1f);
            image.color = color;
            currRuneIndex++;
        }
        SceneManager.LoadScene("scene 2");
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthText.text = "Player Health: " + player.gameObject.GetComponent<dartagnanplayerScript>().currentHealth;
    }
}
