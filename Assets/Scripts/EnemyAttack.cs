using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    RectTransform HealthBarFill;

    public int maxHealth = 100;

    public Player controller;

    public void SetController(Player controller)
    {
        this.controller = controller;
    }

    void SetcurHealth(int curHealth)
    {
        HealthBarFill.localScale = new Vector3(.4f, curHealth, 1f);
        //change healthBar size
        HealthBarFill.transform.localScale = new Vector3(curHealth / 100f * curHealth, HealthBarFill.transform.localScale.y, HealthBarFill.transform.localScale.z);
        //GetComponent<PlayerHealth>().maxHealth -= 1;

    }

    void Update()
    {
        Player controller = GameManager.GetPlayer("Player 2504");
        SetcurHealth(controller.GetcurHealth());
        Debug.Log(controller.GetcurHealth());
    
    }
}