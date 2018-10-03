using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class
// Primarily a shell class containing information needed for multiplayer
public class Player
{
    private int playerIndex; // Used to figure out which input axis to check for stuff. Position in the array of joysticks which Input tracks
    private int points; // how many "points" the player has, for gamemode tie-ins

    public Player()
    {
        playerIndex = 0;
    }
    public Player(int index)
    {
        playerIndex = index;
    }

    // Returns the index corrected to match what joysticks will look like in actual Input button codes
    // Keeps input axes consistent and hopefully readable
    public int GetJoystick()
    {

        return (playerIndex + 1);
    }

    // properties
    public int Index
    {
        get
        {
            return playerIndex + 1;
        }
    }




    public int Points
    {
        get { return points; }
        set
        {
            points = value;
            if (points < 0 )
            {
                points = 0;
            }
        }
    }

}