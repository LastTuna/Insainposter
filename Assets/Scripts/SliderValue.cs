using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {

    public Text valueOnUI;

	// Update is called once per frame
	void Update () {
        valueOnUI.text = Mathf.Round(gameObject.GetComponent<Slider>().value).ToString();


	}
}
