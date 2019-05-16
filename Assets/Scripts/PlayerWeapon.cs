using UnityEngine;

[System.Serializable]   //opens this class to the inspector in order to change properties of each gun
public class PlayerWeapon
{
    public string name = "MP-5";

    public int damage = 10;  //setting damage for gun
    public float range = 100f;  //setting effective distance for gun

    public float fireRate = 0f;

    public int maxAmmo = 25;
    private int currentAmmo;
    private int totalAmmo;
    public float reloadTime = 2f;
    private bool isReloading = false;



    public GameObject model;

    void start()
    {
        currentAmmo = maxAmmo;
    }



    public int ReduceAmmo()
    {
        //   if (this.currentAmmo > 0)
        //  {
        this.totalAmmo--;
        return this.currentAmmo--;
        // }

    }

    public int getTotalAmmo()
    {
        return this.totalAmmo;
    }


    public int GetCurAmmo()
    {
        return this.currentAmmo;
    }

    public void AddTotalAmmo()
    {
        int n = this.totalAmmo / 4;
        this.totalAmmo = this.totalAmmo + n;
    }

    //total
    //public int Get

    public void RelCurAmmo()
    {
        int oldmaxammo = this.maxAmmo;
        if (this.getTotalAmmo() < this.maxAmmo)
        {
            this.maxAmmo = this.getTotalAmmo();
        }
        this.currentAmmo = this.maxAmmo;
        this.maxAmmo = oldmaxammo;
    }

    public void SetRelBool(bool b)
    {
        this.isReloading = b;
    }

    public bool GetRelBool()
    {
        return this.isReloading;
    }
}