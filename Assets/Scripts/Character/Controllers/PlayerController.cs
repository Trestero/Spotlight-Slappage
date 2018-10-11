using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    private Player owner; // Hook for player gameplay class
    private Character pawn; // The Character instance utilized by this controller

    [SerializeField]
    private int debugPlayerIndex = -1; // until we have a menu we'll set the player up this way, -1 is keyboard controls for now

	// Use this for initialization
	void Start ()
    {
        pawn = GetComponent<Character>();

        // TODO: Replace this when we have a way to solidly join the game from like a menu
        //owner = new Player(debugPlayerIndex);
	}
	
    // gets a player passed in, and sets up references between the two
    public void SetOwner(Player plyr)
    {
        owner = plyr;
        owner.AttachedCharacter = gameObject;
    }

	// Update is called once per frame
	void Update ()
    {
        // check to make sure a player's been assigned to this character
		if(owner == null)
        {
            return;
        }

        ReadInput();
	}


    void ReadInput()
    {
        // if anything doesn't check out, return out now
        if(owner == null)
        {
            return;
        }

        if (owner.ControllerNum > 0)
        {
            if (Input.GetJoystickNames().Length < owner.Index || Input.GetJoystickNames()[owner.ControllerNum] == "")
            {
                return;
            }
        }
        // grab horizontal input and plug it into the pawn
        pawn.Move(Input.GetAxis("Horizontal" + owner.GetJoystick()));

        // controller jump
        if (owner.GetJoystick() != 0)
        {
            //if (Input.GetButtonDown("Jump" + owner.GetJoystick()))
            if(Input.GetKeyDown("joystick " + owner.GetJoystick() + " button 1"))
            {
                pawn.Jump();
            }

            // Attack input for controller
            if(Input.GetKeyDown("joystick " + owner.GetJoystick() + " button 0"))
            {
                pawn.Attack();
            }

        }

        // keyboard control
        else
        {
            if (Input.GetAxis("Vertical" + owner.GetJoystick()) > 0)
            {
                pawn.Jump();
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                pawn.Attack();
            }
        }
    }// end of ReadInput

    public Player Owner
    {
        get { return owner; }
    }
}