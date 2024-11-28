using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public static void Die()
    {
        // Handle player death (e.g., reload scene, show game over screen)
        Debug.Log("Player Died");
    }
}
