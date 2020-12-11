using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrader : MonoBehaviour {

    //this class will collect a lot of info about the map on start
    //and we can use this info to give a performance rating 
    //time rating will have to be made manually thou as there is no systematic
    //way to simulate a stage completition
    public GameObject[] enemyEntities;
    //container array for enemy game objects.
    //in the end we check how many of these enemy instances still exist


	// Use this for initialization
	void Start () {
        //here i gather all dolorbehavior and get their game objects and put them in the
        //game object array
        DolorBehavior[] enemies = FindObjectsOfType<DolorBehavior>();
        enemyEntities = new GameObject[enemies.Length];
        for(int i = 0; i < enemies.Length; i++)
        {
            enemyEntities[i] = enemies[i].gameObject;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
