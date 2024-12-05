using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public static Health instance;
    public Slider slider;

    void Awake()
    {
       
    }

    public void Update()
    {
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void Die()
    {
        // Handle player death (e.g., reload scene, show game over screen)
        Debug.Log("Player Died");
    }
}
