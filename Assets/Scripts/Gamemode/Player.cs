using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class
// Primarily a shell class containing information needed for multiplayer
public class Player
{
    private string name;
    private int playerIndex; // Used to figure out which input axis to check for stuff. Position in the array of joysticks which Input tracks
    private int displayIndex; // index adjusted by 1, for use with Unity's systems as well as display onscreen
    private GameObject body; // the gameObject this player is attached to
    private int points; // how many "points" the player has, for gamemode tie-ins

    public Player()
    {
        playerIndex = 0;
        displayIndex = 1;
        points = 0;
        name = "Chad";
    }
    public Player(int index)
    {
        playerIndex = index;
        displayIndex = index + 1;
        points = 0;
        name = "Player " + (displayIndex); 
    }

    // Returns the index corrected to match what joysticks will look like in actual Input button codes
    // Keeps input axes consistent and hopefully readable
    public int GetJoystick()
    {

        return (displayIndex);
    }

    // properties
    public int Index
    {
        get
        {
            return displayIndex;
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