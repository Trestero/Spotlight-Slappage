using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

    private Text scoreText;
    private Player player;


    private bool posSet = false;

	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponentInChildren<Text>();
	}
	
    public void SetTarget(Player plyr)
    {
        player = plyr;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!posSet)
        {
            GetComponent<Image>().color = ConfigInfo.GetColor(player.Index - 1);
            GetComponent<RectTransform>().anchoredPosition = CalcPosition();
            posSet = true;
        }
        if (player != null)
        {
            scoreText.text = player.Name + ": " + player.Points;
        }
	}

    Vector2 CalcPosition()
    {
        return new Vector2(((player.Index - (ConfigInfo.playerCount / 2)) * 160) - (80 + (75 * (ConfigInfo.playerCount % 2))), 20);
    }
}
