﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slapper : MonoBehaviour {

    public float RotSpeed;
    public int rotTimer;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        rotTimer = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.activeSelf)
        {
            if(rotTimer > 0)
            {
                gameObject.transform.rotation = 
                    new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y + RotSpeed, gameObject.transform.rotation.z, 1);
                rotTimer--;
            }
            else
            {
                if(RotSpeed>0)
                {
                    gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, -50, gameObject.transform.rotation.z, 1);
                }
                else
                {
                    gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, 50, gameObject.transform.rotation.z, 1);
                }
                gameObject.SetActive(false);
                rotTimer = 10;
            }
        }
	}
}