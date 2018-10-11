using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    private Text scoreText;
    private Transform trackTarget;
    private Player player;

    [SerializeField]
    private Vector3 offset = new Vector3(0, .8f, 0);


    private Camera cam;
	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponentInChildren<Text>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
    public void SetTarget(Player plyr)
    {
        player = plyr;
        trackTarget = plyr.AttachedCharacter.transform;
    }

	// Update is called once per frame
	void Update ()
    {
		if(player != null)
        {
            scoreText.text = player.Name + ": " + player.Points;
        }

        // if there's a target to seek towards
        if(trackTarget != null)
        {
            GetComponent<RectTransform>().anchoredPosition = cam.WorldToScreenPoint(trackTarget.position + offset);
        }
	}
}
