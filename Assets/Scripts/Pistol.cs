using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {

    public GameObject confetti;
    //edit this, add the wormhole sprite instead
    public const int weaponPower = 100;
    //pistol can kill every weak enemy with 1 shot right now


    public IEnumerator ShootPistol(Vector3 playerPos, Vector3 playerDir, System.Action<bool> callback)
    {
        //shoot 4 raycasts
        //if first doesnt hit then try second angle then third
        RaycastHit hit;
        float angle = 0;
        while (angle < 0.8f)
        {
            Vector3 aimAngle = new Vector3(0, angle, 0);
            if(Physics.Raycast(playerPos, (playerDir + aimAngle) * 360, out hit, 50))
            {
                if (hit.collider.tag == "Enemy")
                {
                    //if the hit is lethal, print the wormhole, otherwise just do damage
                    if (hit.collider.gameObject.GetComponent<DolorBehavior>().health < 100)
                    {
                        //add the wormhole sprite instantiate here
                        //TEMPORARY DEBUG INSTANTIATE CONFETTI
                        Instantiate(confetti, hit.point, hit.transform.rotation);
                    }
                    hit.collider.gameObject.GetComponent<DolorBehavior>().Damage(100);
                    break;
                }
            }
            angle += 0.2f;
        }
        yield return new WaitForSeconds(0.8f);
        callback(true);
    }

}
