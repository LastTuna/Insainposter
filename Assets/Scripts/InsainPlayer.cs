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

    private const float loBob = 25;
    //loBob controls the lowest the camera will bob
    //lobob also controls the RATE of camera bob


    // Use this for initialization
    void Start () {
        defaultCamPos = HeadCam.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Bobbing();
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
    }
    
    public void Turning()
    {
        float horizontalRot = Input.GetAxis("Mouse X");
        gameObject.transform.Rotate(0, horizontalRot * mouseSensitivity, 0);
    }
    
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
