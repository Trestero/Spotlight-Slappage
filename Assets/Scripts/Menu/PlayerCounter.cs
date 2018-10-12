using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCounter : MonoBehaviour {

    Text counter;
	// Use this for initialization
	void Start ()
    {
        counter = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        counter.text = "" + ConfigInfo.playerCount;
	}
}
