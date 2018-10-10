using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject lightPrefab;

    private SpotlightTracker light;
    private Manager manager;
	// Use this for initialization
	void Start ()
    {
        light = GameObject.Instantiate(lightPrefab, transform.position, Quaternion.identity).GetComponent<SpotlightTracker>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            manager.instance.SetLight(light);
            manager.instance.PlayerWithFocus = collision.GetComponent<PlayerController>().Owner;
            gameObject.SetActive(false);
        }
    }
}
