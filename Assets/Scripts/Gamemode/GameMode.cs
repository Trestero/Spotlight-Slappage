using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void WinnerDeclaredEvent();

public abstract class GameMode {

    private Player[] players;
    private Player hasFocus; // which player has the spotlight
    protected bool inProgress;
    public event WinnerDeclaredEvent OnWin; // callback event for when the game is over
    protected SpotlightTracker light; // the spotlight

    // straightforward setup method for the gamemode, called when the mode is loaded up
    public virtual void Setup(int playerCount)
    {
        players = new Player[playerCount];
        for (int i = 0; i < playerCount; i++)
        {
            players[i] = new Player(ConfigInfo.inputIndices[i]); // get the control index used by this player and feed it in
            players[i].Name = "P" + (i + 1);
        }
        
    }

    // for setting up which spotlight to use
    public void SetLight(SpotlightTracker lt)
    {
        light = lt;
    }

    // accessor for player list, should never be accessible
    public Player[] GetPlayers()
    {
        return players;
    }

    // which player has the spotlight
    public Player PlayerWithFocus
    {
        get
        {
            return hasFocus;
        }
        set
        {
            hasFocus = value;
            // set target of the spotlight
            light.SetTarget(value.AttachedCharacter.transform);
        }
    }

    // Returns which player is in the lead
    public virtual Player GetLeader()
    {
        Player max = players[0];
        for (int i = 1; i < players.Length; i++)
        {
            if(players[i].Points > max.Points)
            {
                max = players[i];
            }
        }

        if(max.Points == 0)
        {
            return null;
        }
        return max;
    }

    // when some win condition is achieved, this method ends the game and does cleanup
    protected virtual void EndGame()
    {
        inProgress = false;
        OnWin();
    }

    public virtual void Start()
    {
        inProgress = true;
    }

    public virtual void Update(float deltaTime)
    {
        if (!inProgress)
        {
            return;
        }
    }
}