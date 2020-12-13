using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour {

    public string type;
    //types of interactables
    //BUTTON, DOOR 
        
    public string key;
    //store the string key value. if empty, it doesnt require anything to open.
    //example: "Blue Key" or "KeyCard"
    public bool locked;
    //just making sure youre not tryna open the fuckin same door idiot
    
    public GameObject Keyframe0;
    public GameObject keyframe1;
    //where the door transform goes to and from
    float currentKeyFrame = 0;
	
    //!!!!!DONT FORGET TO TAG YOUR INTERACTABLES WITH THE "INTERACTABLE" TAG!

        //move the door if its unlocked mane
        //tbh sort out something more intelligent later but just fuckin make it WORK PAL
        //make an ienumerator and call a coroutine or something to interpolate the door in place on call instead of checking every update if the door is unlocked etc
	void FixedUpdate () {
        if (!locked && currentKeyFrame < 1)
        {
            Debug.Log("OPENEING DOOR,,,,");
            currentKeyFrame += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(keyframe1.transform.position, Keyframe0.transform.position, currentKeyFrame);
        }


	}
}
