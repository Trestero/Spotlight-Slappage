using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slapper : MonoBehaviour {

    public float RotSpeed;
    public int rotTimer;
    public GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
        rotTimer = 10;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rotTimer > 0 && collision.gameObject.CompareTag("Player") && gameManager.GetComponent<GameMode>().PlayerWithFocus == collision.gameObject.GetComponent<Player>() )
        {
            gameManager.GetComponent<GameMode>().PlayerWithFocus = transform.parent.gameObject.GetComponent<Player>();
            if(RotSpeed > 0) //knockback
            {
                collision.gameObject.transform.position += new Vector3(0.0f, 5.0f);
            }
            else
            {
                collision.gameObject.transform.position += new Vector3(0.0f, -5.0f);
            }
        }
    }
}
