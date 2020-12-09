using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : MonoBehaviour {

    public Transform baseballBatKey;
    public GameObject confetti;
    
    public IEnumerator BatSwing(System.Action<bool> callback)
    {
        float batSwing = 0;
        int i = 0;//just make this so it doesnt call a raycast every tick.
        Quaternion originalRot = gameObject.transform.localRotation;
        Vector3 originalPos = gameObject.transform.localPosition;

        while (batSwing < 1)
        {
            if(i > 2)
            {
                hitDetect();
                i = 0;
            }
            batSwing += Time.deltaTime;
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, baseballBatKey.localPosition, batSwing);
            gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, baseballBatKey.localRotation, batSwing);
            yield return new WaitForEndOfFrame();
            i++;
        }
        batSwing = 0;
        while (batSwing < 1)
        {
            batSwing += Time.deltaTime;
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, originalPos, batSwing);
            gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, originalRot, batSwing);
            yield return new WaitForEndOfFrame();
        }
        yield return false;
        callback(true);
    }

    void hitDetect()
    {
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position, gameObject.transform.forward * 360, out hit, 3f))
        {
            if(hit.collider.tag == "Enemy")
            {
                Instantiate(confetti, hit.collider.gameObject.transform.position + Vector3.up, new Quaternion(0, 0, 0, 0));
                Destroy(hit.collider.gameObject);
            }
        }
    }

}
