using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider HealthBar;
    public int maxHealth = 100;

    private int curHealth;

    void Start()
    {
        curHealth = maxHealth;
    }

    //public void RpcTakeDamage(int damage)
    //{
      //  curHealth -= damage;
       // HealthBar.value = curHealth;
       
    //}
}