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
        //PISTOLammo, GUN2ammo, RIFTcartridge
    //ACTION ITEMS:
        //literally what ever else. "Blue Key", "Red Key", etc
    

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

            case "PISTOLammo":
                FindObjectOfType<WeaponManager>().weaponAmmo[0] += value;
                break;
            case "GUN2ammo":
                FindObjectOfType<WeaponManager>().weaponAmmo[0] += value;
                break;
            case "RIFTcartridge":
                FindObjectOfType<WeaponManager>().weaponAmmo[0] += value;
                break;

                //if its not any of the above, then it is probably an action item/pickup
                //such as keys. add item to inventory.
            default:
                FindObjectOfType<InsainPlayer>().AddItem(type);
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
