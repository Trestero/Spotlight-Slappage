using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player class
// Primarily a shell class containing information needed for multiplayer
public class Player
{
    private string name;
    private int controllerIndex; // Used to figure out which input axis to check for stuff. Position in the array of joysticks which Input tracks
    private int displayIndex; // index adjusted by 1, for use with Unity's systems as well as display onscreen
    private GameObject body = null; // the gameObject this player is attached to
    private int points; // how many "points" the player has, for gamemode tie-ins

    public Player()
    {
        controllerIndex = 0;
        displayIndex = 1;
        points = 0;
        name = "Chad";
    }
    public Player(int index)
    {
        controllerIndex = index;
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

    public int ControllerNum
    {
        get
        {
            return controllerIndex;
        }
    }

    public GameObject AttachedCharacter
    {
        get
        {
            return body;
        }
        set
        {
            body = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
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