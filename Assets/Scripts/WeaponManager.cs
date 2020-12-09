using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public int currentWeapon;
    public bool[] availableWeapons = new bool[4];
    //gun list:
    //0 = dimensional pistol
    //1 = TO BE ADDED WHATEVER MID LEVEL WEAPON
    //2 = HOLE RIPPER 3000
    //3 = PIÑATA BASEBALL BAT
    public GameObject[] weapons;
    //container for all the weapon instances.
    public int[] weaponAmmo = new int[4];
    //weapon ammo array
    public bool firing = false;
    //halt weapon swaps, and shooting when true
    //firing is also true if weapon is being changed/drawn

    private Vector3 weaponOGpos;
    //weapon orig position store
    public float weaponDrawLerp;
    public BaseballBat baseballbat;
    public Pistol pistol;

    // Use this for initialization
    void Start () {
        availableWeapons[0] = true;
        weaponAmmo[0] = 30;
        weaponAmmo[3] = -1;
        //make default weapon, dimensional pistol available
	}
	
    //penis music
    //add scrollwheel support if u want ig
	void FixedUpdate () {
        
        if (Input.GetAxis("Fire1") > 0 && !firing)
        {
            firing = true;
            if(weaponAmmo[currentWeapon] != 0)
            {
                UseWeapon();
                Debug.Log("POW!HAHAAAAAAAA");
            }
            else
            {
                firing = false;
                Debug.Log("out of ammo dear");
            }
        }
        //call this after firing in the case that user fires because we dont want to swap weapon and fire at the same time
        if (!firing)
        {
            WeaponKeySelector();
        }
    }

    void UseWeapon()
    {
        weaponAmmo[currentWeapon]--;
        switch (currentWeapon)
        {
            case 0:
                StartCoroutine(pistol.ShootPistol(gameObject.transform.position, gameObject.transform.forward, (pow) => {
                    if (pow) firing = false;
                }));
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                //this is a monster
                //call coroutine from baseballbat, once its done it will callback a false
                StartCoroutine(baseballbat.BatSwing((swingin) => {
                    if (swingin) firing = false; }));
                break;
        }

    }

    void WeaponKeySelector()
    {
        int oldWeapon = currentWeapon;
        if (Input.GetKey("1") && availableWeapons[0])
        {
            currentWeapon = 0;
        }
        else if (Input.GetKey("2") && availableWeapons[1])
        {
            currentWeapon = 1;
        }
        else if (Input.GetKey("3") && availableWeapons[2])
        {
            currentWeapon = 2;
        }
        else if (Input.GetKey("4") && availableWeapons[3])
        {
            currentWeapon = 3;
        }

        if(oldWeapon != currentWeapon)
        {
            DrawWeapon(oldWeapon);
        }
    }

    //toggle old weapon inactive/invisible
    //draw new weapon. store weapons original pos in the container variable
    //then move the weapon down 1, then set it visible.
    //then call lerp coroutine to add cool draw effect. after coroutine is done firing = false
    void DrawWeapon(int oldWeapon)
    {
        firing = true;
        weapons[oldWeapon].SetActive(false);
        weaponOGpos =  weapons[currentWeapon].transform.localPosition;
        weapons[currentWeapon].transform.localPosition += new Vector3(0, -1, 0); 
        weapons[currentWeapon].SetActive(true);
        weaponDrawLerp = 0;
        StartCoroutine(DrawAnimation());
    }

    IEnumerator DrawAnimation()
    {
        while(weaponDrawLerp < 1)
        {
            weaponDrawLerp += Time.deltaTime;
            weapons[currentWeapon].transform.localPosition = Vector3.Lerp(weapons[currentWeapon].transform.localPosition, weaponOGpos, weaponDrawLerp);
            yield return new WaitForEndOfFrame();
        }
        firing = false;
        yield return null;
    }


}
