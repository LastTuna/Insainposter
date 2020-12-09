using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

    public Sprite[] faces;
    public Sprite[] weaponNumSprites;
    public Image[] weaponNumbersOnUI;
    public Image FaceOnUI;
    public Text Health;
    public Text Armor;
    public Text Ammo;
    private WeaponManager weaponData;
    private InsainPlayer player;

	// Use this for initialization
	void Start () {
        weaponData = FindObjectOfType<WeaponManager>();
        player = FindObjectOfType<InsainPlayer>();
    }
	
	// Update is called once per frame
	void Update () {

        FaceMan();
        Health.text = Mathf.Round(player.health).ToString();
        Armor.text = Mathf.Round(player.armor).ToString();

        if(weaponData.currentWeapon == 3)
        {
            Ammo.text = "-";
        }
        else
        {
            Ammo.text = weaponData.weaponAmmo[weaponData.currentWeapon].ToString();
        }

    }

    //face manager. (to be expanded?) more interpolation frames, and possibly a mouse lerp
    public void FaceMan()
    {
        if (Input.GetAxis("Mouse X") > 0.4f)
        {
            FaceOnUI.sprite = faces[2];
        }
        else if (Input.GetAxis("Mouse X") < -0.4f)
        {
            FaceOnUI.sprite = faces[0];
        }
        else
        {
            FaceOnUI.sprite = faces[1];
        }
    }

    //weaponNumSprites only contains the sprite instances for unlocked, so just access them
    //with the same index. cant really lock a weapon again but why would you do that?
    public void UnlockWeapon(int weapon)
    {
        weaponNumbersOnUI[weapon].sprite = weaponNumSprites[weapon];
    }

}
