using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    private Text scoreText;
    private Transform trackTarget;
    private Player player;

    private Camera cam;

    private bool posSet = false;

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
        if (!posSet)
        {
            GetComponent<Image>().color = ConfigInfo.GetColor(player.Index - 1);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(((player.Index - (ConfigInfo.playerCount / 2)) * 160) - 80, 20);
            posSet = true;
        }
        if (player != null)
        {
            scoreText.text = player.Name + ": " + player.Points;
        }
	}

}
