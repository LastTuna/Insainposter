using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolorBehavior : MonoBehaviour {

    public float health = 2;
    public bool sight = false;
    public float movementSpeed = 0.1f;//max movement speed
    private float currentSpeed = 0f;
    public float gravity = -0.2f;//gravity- leave at 0 for a flying monster (think:cacodemon)
    private float gravitySpeed = 0;
    public float shootRate = 1f;//shoot rate in seconds
    public GameObject projectile;//put the projectile prefab here
    //if projectile == null then dont shoot nuffin obv
    private bool shooting;
    //shooting will work via a coroutine since we are using seconds

    public CharacterController entity;
    //THIS instances character controller
    public GameObject player;
    //the PLAYER game object NOT THE MOB

    public float minDistance;
    //min distance: crossing this threshold will make ai walk back
    public float maxDistance;
    //max distance: how far the ai can be before it will start walking towards you.
    //PUT MIN AND MAX DISTANCE TO 0 FOR MELEE MOBS

	// Use this for initialization
	void Start () {
        player = GameObject.Find("PLAYER");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Linecast(transform.position, player.transform.position, out hit))
        {
            if(hit.collider.name == "PLAYER")
            {
                sight = true;
                StrayBehavior(hit.distance);
                //make sprite look at player
                gameObject.transform.LookAt(player.transform.position);
            }
            else
            {
                sight = false;
            }
        }

        //calc gravity
        Vector3 grav = new Vector3(0, gravitySpeed, 0);
        if (!entity.isGrounded)
        {
            gravitySpeed += gravity * Time.deltaTime;
            grav = new Vector3(0, gravitySpeed, 0);
        }
        else
        {
            gravitySpeed = 0;
        }
        entity.Move(grav);

        if(sight && projectile != null && !shooting)
        {
            StartCoroutine(ShootProjectile());
        }
        //if enemy runs out of health it dies DUH
        if(health < 0)
        {
            Destroy(gameObject);
        }

    }

    IEnumerator ShootProjectile()
    {
        shooting = true;
        Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(shootRate);
        shooting = false;
        yield return null;
    }

    void StrayBehavior(float distance)
    {
        //katsotaan raycastilla mihin suuntaan paetaan

        Vector3 direction = Vector3.Normalize(player.transform.position - gameObject.transform.position);
        //precalc & normalize direction vector. multiply by 360 when used in raycast

        RaycastHit culos;
        Physics.Raycast(transform.position, direction * 360, out culos);
        currentSpeed = 0;
        //if user is too close/straying away
        if (distance < minDistance)
        {
            currentSpeed = -movementSpeed;
        }
        //user is too far, lets go getum tyron
        if(distance > maxDistance)
        {
            currentSpeed = movementSpeed;
        }
        entity.Move(direction * currentSpeed);

    }
    
    public void Damage(int damage)
    {
        //things you can expand this with:
        //enemy damage animation
        //whatever else you can come up with
        health = -damage;
    }

}

