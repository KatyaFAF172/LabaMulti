using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform Ammo;

    public Text AmmoText;
    //public UnityEngine.UI.Text uiText = null;

    private PlayerController controller;
    private Player player;
    private Text currentAmmo; //InputField
    private Text maxAmmo;
    public PlayerWeapon weapon;

    public void SetController(PlayerController controller)
    {
        this.controller = controller;

    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }



    void SetFuelAmount (float amount)
    {
        thrusterFuelFill.localScale = new Vector3(1f, amount, 1f);
    }

    private void SetcurrentAmmo(int amount)
    {
        //currentAmmo = (InputField) amount / 100;
       

    }

    void Update()
    {
        //SetFuelAmount(controller.GetThrusterFuelAmount());
        //Debug.Log(transform.name + " now has " + currentAmmo + " bullet(s)");

        if (currentAmmo != null)
        {
            currentAmmo.text = currentAmmo.ToString();
        }

        //currentAmmo.text = currentAmmo + "/" + maxAmmo;
        //if (uiText != null) uiText.text = ammoGuiToString() + "/"
        SetcurrentAmmo(weapon.GetCurAmmo());
    }

   
}
