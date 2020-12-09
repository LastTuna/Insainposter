using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {

    public string type = "HEALTH";
    public int value = 0;

    //UTILITY:
        //HEALTH, ARMOR
    //DROPS:
        //RIPPER, GUN2, BASEBALLBAT
    //AMMO:
        //AMMO0, AMMO1, AMMO2
    //

    //if you forget to conf the drop
    //it just goes to health by default

    // Update is called once per frame
    void Action()
    {
        switch (type)
        {
            case "HEALTH":
                FindObjectOfType<InsainPlayer>().health += value;
                break;
            case "ARMOR":
                FindObjectOfType<InsainPlayer>().armor += value;
                break;

            case "GUN2":
                FindObjectOfType<UIControl>().UnlockWeapon(1);
                FindObjectOfType<WeaponManager>().availableWeapons[1] = true;
                break;
            case "RIPPER":
                FindObjectOfType<UIControl>().UnlockWeapon(2);
                FindObjectOfType<WeaponManager>().availableWeapons[2] = true;
                break;
            case "BASEBALLBAT":
                FindObjectOfType<UIControl>().UnlockWeapon(3);
                FindObjectOfType<WeaponManager>().availableWeapons[3] = true;
                break;

            case "AMMO0":
                FindObjectOfType<WeaponManager>().weaponAmmo[0] += value;
                break;

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "PLAYER")
        {
            Action();
            Destroy(gameObject);
        }
    }
    
}
