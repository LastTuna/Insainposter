using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DolorProjectile : MonoBehaviour {

    //enemy projectile. this will be on a projectile prefab instance the enemy spawns
    //basic behavior is that its a trigger object that moves forward linearly
    //if trigger hits something it will check if its a player, adds damage to player
    //then despawns

    public float projectileSpeed = 0.7f;
    public int damage = 10;
    
	void FixedUpdate () {
        gameObject.transform.position += projectileSpeed * gameObject.transform.forward * Time.deltaTime;

	}

    //im 3 beers in so this is what you get for this detection
    //ignore enemy collsision because all projectiles are spawned inside the enemy sop yea
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("le australian hit" + other.tag + other.name);

        if(other.name == "PLAYER")
        {
            Debug.Log("narr the aussie done glass u");
            FindObjectOfType<InsainPlayer>().Damage(damage);
            Destroy(gameObject);
        }

        if (other.tag != "Enemy") Destroy(gameObject);
    }
}
