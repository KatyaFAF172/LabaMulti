using UnityEngine.Networking;
using UnityEngine;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeapon PrimaryWeapon;
    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform WeaponHolder;

    private PlayerWeapon curWeapon;
    private WeaponGraphics currentGraphics;

    void Start()
    {
        EquipWeapon(PrimaryWeapon);
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return curWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    void EquipWeapon(PlayerWeapon weapon)
    {
        curWeapon = weapon;

        GameObject weaponIns = (GameObject)Instantiate(weapon.model, WeaponHolder.position, WeaponHolder.rotation);
        weaponIns.transform.SetParent(WeaponHolder);

        currentGraphics = weaponIns.GetComponent<WeaponGraphics>();
        if (!currentGraphics)
        {
            Debug.LogError("No WeaponGraphics component on the weapon object");
        }

        if (isLocalPlayer)
            Util.SetLayerRecursively(weaponIns, LayerMask.NameToLayer(weaponLayerName));
                //weaponIns.layer = LayerMask.NameToLayer(weaponLayerName);
    }
}
