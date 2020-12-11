using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolorProjectile : MonoBehaviour {

    //enemy projectile. this will be on a projectile prefab instance the enemy spawns
    //basic behavior is that its a trigger object that moves forward linearly
    //if trigger hits something it will check if its a player, adds damage to player
    //then despawns

    public float projectileSpeed = 0.7f;

    


	// Use this for initialization
	void Start () {
		
	}
	

	void FixedUpdate () {
        gameObject.transform.position += projectileSpeed * gameObject.transform.forward * Time.deltaTime;
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "PLAYER")
        {
            //call player damage here
        }
        Destroy(gameObject);
    }

}
