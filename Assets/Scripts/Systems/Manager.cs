using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // stuff regarding player spawning
    [Header("Player Spawn Info")]
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private List<Transform> spawnLocations;

    [SerializeField]
    private GameObject winPrefab;

    private UIManager uiManager;

    [SerializeField]
    private Sprite[] playerSprites = new Sprite[3];

    // information to use in setting up gamemodes
    [Header("Game Mode Configurations"), Space]
    [Header("Timed Mode Settings")]
    [Tooltip("Length of the round timer in seconds.")]
    public float timedModeTimer = 180;

    public GameMode instance; // the instance of the game to be initialized


	// Use this for initialization
	void Start ()
    {
        uiManager = GetComponent<UIManager>();
		switch(ConfigInfo.currentGameMode)
        {
            // Timed mode
            case Mode.Timed:
                {
                    instance = new TimedMode();
                    ((TimedMode)instance).Setup(ConfigInfo.playerCount, timedModeTimer);
                    break;
                }
        }

        // hook up win event to the display function
        instance.OnWin += DisplayWinner;

        SetupGame();

        // start the round instance
        instance.Start();
	}
	
    private void SetupGame()
    {
        for(int i = 0; i < instance.GetPlayers().Length; i++)
        {
            if (spawnLocations.Count > 0) // do nothing if there are no spawn locations to use
            {
                SpawnPlayer(instance.GetPlayers()[i], spawnLocations[i % spawnLocations.Count].position);
            }
        }

    }

    // given a player and spawnlocation, sets up a character controlled by the player
    private void SpawnPlayer(Player playerToAssign, Vector3 location)
    {
        GameObject obj = GameObject.Instantiate(playerPrefab, location, Quaternion.identity);
        obj.GetComponent<PlayerController>().SetOwner(playerToAssign);
        // set sprite based on what the player chose
        obj.GetComponent<SpriteRenderer>().sprite = playerSprites[ConfigInfo.characters[playerToAssign.Index - 1]];


        // passes the player reference to the UI Manager to construct UI elements
        uiManager.AddPlayerUI(playerToAssign);

    }

	// Update is called once per frame
	void Update ()
    {
		switch(ConfigInfo.currentGameMode)
        {
            // Timed mode
            case Mode.Timed:
                {
                    instance.Update(Time.deltaTime);
                    break;
                }
        }
	}

    void DisplayWinner()
    {
        GameObject go = GameObject.Instantiate(winPrefab, GameObject.Find("Canvas").transform);
        if(instance.GetLeader() != null)
        go.GetComponent<UnityEngine.UI.Text>().text = instance.GetLeader().Name + " Wins!";
    }
    
    // return whether the player in question has the light's focus
    bool PlayerHasLight(Player plyr)
    {
        return (instance.PlayerWithFocus == plyr);
    }
}
