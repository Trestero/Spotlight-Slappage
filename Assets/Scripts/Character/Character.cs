using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// What Tom's comments mean:
// Actual comments
/// Explanations of why I'm doing stuff which may not otherwise be apparent


/// Using RequireComponent before class definition means that putting this script on a GameObject will automatically add any missing required components
/// This isn't great for abstract/varied stuff like colliders but it works well for rigidbodies which are applied very generally
[RequireComponent(typeof(Rigidbody2D))]



/// A basic Character class with "hooks" for a character controller to interface with
/// Contains all information relating to the character itself, but isolates the input
/// This allows different character controllers to be attached to it, which would theoretically allow us to implement a CPU by swapping only the controller script
public class Character : MonoBehaviour
{
    // Unserialized fields don't appear in the inspector
    private Rigidbody2D rb;

    private bool grounded; // whether the character is on the ground

    [SerializeField]
    private Sprite[] playerSprites;
    private SpriteRenderer displaySprite;

    // Movement value fields
    [Header("Movement Attributes")]
    [SerializeField]
    private float walkSpeed = 1f; // walk speed in units/second
    [SerializeField]
    private float jumpHeight = 3f;

    private float jumpVelocity;

    // slapper objs
    public GameObject slapperLeft;
    public GameObject slapperRight;

    private bool facingR = false;

    // stuff for invincibility and knockback


    // Use this for initialization
    void Start ()
    {
        // grab component references
        rb = GetComponent<Rigidbody2D>();
        displaySprite = GetComponent<SpriteRenderer>();
        Vector3 dir = Vector3.zero - transform.position;
        if (dir.x > 0)
        {
            facingR = true;
        }
        else
        {
            facingR = false;
        }

        SetSprite(facingR);
    }

    public void SetSprite(bool facingRight)
    {
        if(facingRight)
        {
            //displaySprite.sprite = playerSprites[0];
            displaySprite.flipX = true;
        }
        else
        {
            //displaySprite.sprite = playerSprites[1];
            displaySprite.flipX = false;
        }
    }

    public void Move(float dir) // takes in a float from -1 to 1 and applies walk speed, then turns that into horizontal velocity on the rigidbody
    {
        dir = Mathf.Clamp(dir, -1f, 1f); // clamp to ensure the input is treated as an input axis properly
        if (grounded) // if on the ground, simply convert input to walk speed
        {
            rb.velocity = new Vector2(rb.velocity.x + (dir * walkSpeed * (Time.deltaTime * 5f)), rb.velocity.y);

        }
        else // if airborne, allow some horizontal momentum to factor in
        {
            rb.velocity = new Vector2(rb.velocity.x + (dir * walkSpeed * (Time.deltaTime * 2f)), rb.velocity.y);
        }

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -walkSpeed, walkSpeed), rb.velocity.y);

        if (dir > 0)
        {
            facingR = true;
        }
        else if(dir < 0)
        {
            facingR = false;
        }

        SetSprite(facingR);
    }

    public void Move(Vector2 dir) // on the off chance we want a flying character down the line, this overloaded version allows 2 dimensions of movement
    {
        dir.Set(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1));
        rb.velocity = dir * walkSpeed;
    }

    public void Jump()
    {
        if(!grounded) // if in the air, exit the function immediately
        {
            return;
        }

        // get required initial velocity to jump to a given height
        // V^2 = Vo^2 + 2ad   |    0 = Vo^2 + 2ad    |    Vo^2 = -2ad
        // where d = jumpHeight and a is the magnitude of gravity
        /// to make the jump height look natural, we can use this to solve for the necessary initial velocity
        /// This is computationally pretty inefficient, so once we have a solid jump height nailed down for characters we should do this in Start()
        /// For now this allows quick iteration and changing jumpHeight during editor playmode
        jumpVelocity = Mathf.Sqrt(-Physics2D.gravity.y * jumpHeight * 2);


        // jump to a given height in the direction opposite gravity
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        grounded = false;
    }


    // do attacky things
    public void Attack()
    {
        if(facingR && slapperRight.activeSelf==false)
        {
            slapperRight.SetActive(true); 
            PlaySwingSound();
        }
        if (!facingR && slapperLeft.activeSelf == false)
        {
            slapperLeft.SetActive(true);
            PlaySwingSound();
        }
    }

    private void PlaySwingSound()
    {
        GameObject.Find("SwingFXPlayer" + Random.Range(1, 3)).GetComponent<AudioSource>().Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(grounded && (collision.gameObject.CompareTag("LevelBlock") || collision.gameObject.CompareTag("Player")))
        {
            grounded = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!grounded && (collision.gameObject.CompareTag("LevelBlock") || collision.gameObject.CompareTag("Player")))
        {
            grounded = true;
        }
    }

}// END OF CHARACTER CLASS