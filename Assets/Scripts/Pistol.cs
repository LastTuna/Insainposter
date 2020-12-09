using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {

    public GameObject confetti;


    public IEnumerator ShootPistol(Vector3 playerPos, Vector3 playerDir, System.Action<bool> callback)
    {
        //shoot 4 raycasts
        //if first doesnt hit then try second angle then third
        RaycastHit hit;
        float angle = 0;
        Debug.Log(playerPos + " PENIS " + playerDir);
        while (angle < 0.8f)
        {
            Vector3 aimAngle = new Vector3(0, angle, 0);
            Debug.Log((aimAngle + playerDir) * 360);
            if(Physics.Raycast(playerPos, (playerDir + aimAngle) * 360, out hit, 50))
            {
                //TEMPORARY DEBUG INSTANTIATE
                Instantiate(confetti, hit.point, hit.transform.rotation);
                if (hit.collider.tag == "Enemy")
                {
                    //add the explosion effect here
                    Destroy(hit.collider.gameObject);
                    break;
                }
            }
            angle += 0.2f;
        }
        yield return new WaitForSeconds(0.8f);
        callback(true);
    }

}
