using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedMode : GameMode {

    private float roundTimer; // how long a round lasts in seconds
    private float pointTimer = 0.0f;

    public void Setup(int playerCount, float time)
    {
        base.Setup(playerCount);
        roundTimer = time;
    }

    public override void Update(float deltaTime)
    {
        // if not running the game, do nothing
        if(!inProgress)
        {
            return;
        }

        roundTimer -= deltaTime;
        if(roundTimer <= 0)
        {
            roundTimer = 0.0f; // for nice display purposes, stop at 0.0s
            EndGame();
        }


        // if a second has passed, increment the score of the player who has the spotlight
        pointTimer += deltaTime;
        if(pointTimer >= 1.0f)
        {
            pointTimer = 0f;
            if (PlayerWithFocus != null)
            {
                PlayerWithFocus.Points++;
            }
        }
    }

    public float TimeRemaining
    {
        get { return roundTimer; }
    }

}
