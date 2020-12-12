using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsainPlayer : MonoBehaviour {

    public Vector3 momentum;
    //possibly used for explosions(?) or other external forces.
    public float movementSpeed = 0.2f;
    public CharacterController character;
    public GameObject HeadCam;
    public float gravity = -0.2f;
    private float gravitySpeed = 0;
    public float mouseSensitivity = 2;
    public float health = 100;
    public float armor = 0;
    public Vector3 defaultCamPos;
    private float bobFactor;
    private bool bobDir;
    
    public bool paused;
    public List<string> inventory;

    //to pause bobbing animation

    private const float loBob = 25;
    //loBob controls the lowest the camera will bob
    //lobob also controls the RATE of camera bob


    // Use this for initialization
    void Start () {
        defaultCamPos = HeadCam.transform.localPosition;
        DataController dolor = FindObjectOfType<DataController>();
        HeadCam.GetComponent<Camera>().fieldOfView = dolor.LoadedData.FOV;
        mouseSensitivity = dolor.LoadedData.MouseSensitivity;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!paused)
            {
                Bobbing();
            }
        }
	}

    //fixedupdate is called every physics tick
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(0,0,0);
        movement = (gameObject.transform.right * Input.GetAxis("Horizontal")) + (gameObject.transform.forward * Input.GetAxis("Vertical"));

        Vector3 grav = new Vector3(0, gravitySpeed, 0);
        

        if (!character.isGrounded)
        {
            gravitySpeed += gravity * Time.deltaTime;
            grav = new Vector3(0,gravitySpeed,0);
        }
        else if (Input.GetAxis("Jump") > 0)
        {
            //jumping mechanic? added here anyway.
            gravitySpeed = 0.1f;
        }
        else
        {
            gravitySpeed = 0;
        }
        character.Move(momentum + movement * movementSpeed);
        character.Move(grav);
        Turning();

        if (health <= 0)
        {
            Debug.Log("you are dead bitch)");
            paused = true;
            FindObjectOfType<UIControl>().PauseGame();
            FindObjectOfType<UIControl>().YoureDead();
            //ur fukin dead kid
        }

        //interaction button handler(mouse right)
        if (Input.GetAxis("Fire2") > 0)
        {
            ActionButton();
        }

    }

    void ActionButton()
    {
        //this is the interaction handler for using keys and utilities
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.rotation.eulerAngles, out hit, 2.0f))
        {
            if(hit.collider.tag == "Interactable")
            {
                //no idea what to put here yet



            }
        }
    }

    public void AddItem(string item)
    {
        inventory.Add(item);
    }

    public void RemoveItem(string item)
    {
        inventory.Remove(item);
    }

    
    //mouse control rotation etc
    public void Turning()
    {
        float horizontalRot = Input.GetAxis("Mouse X");
        gameObject.transform.Rotate(0, horizontalRot * mouseSensitivity, 0);
    }
    
    public void Damage(int damage)
    {
        //flash screen red when u get dmg, make some gradient pic and slice it in unity
        //and also damage handler when you touch enemy (use enemy.tag) and when u get hit
        //by enemy projectile OR raycast(?)
        //this method is currently called from:
        //DolorProjectile.cs
        FindObjectOfType<UIControl>().DamageFlasher();
        health -= damage;

    }

    //camera bob
    public void Bobbing()
    {

        if (bobFactor == loBob)
        {
            bobDir = false;
        }
        if (bobFactor == 0)
        {
            bobDir = true;
        }

        if (bobDir)
        {
            bobFactor++;
        }
        else
        {
            bobFactor--;
        }
        HeadCam.transform.localPosition = new Vector3(defaultCamPos.x,
            defaultCamPos.y - (bobFactor / 100),
            defaultCamPos.z);
    }


}
